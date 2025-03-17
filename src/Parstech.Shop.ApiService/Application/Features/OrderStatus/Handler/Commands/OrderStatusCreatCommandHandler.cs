using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.ApiService.Application.Generator;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderStatus.Handler.Commands;

public class OrderStatusCreatCommandHandler : IRequestHandler<OrderStatusCreatCommandReq, ResponseDto>
{
    private readonly IOrderStatusRepository _orderStatusRepo;
    private readonly IUserRepository _userRep;
    private readonly IOrderDetailRepository _orderDetailRep;
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public OrderStatusCreatCommandHandler(IOrderStatusRepository orderStatusRepo,
        IProductStockPriceRepository productStockPriceRep,
        IMapper mapper,
        IMediator mediator,
        IUserRepository userRep,
        IOrderDetailRepository orderDetailRep)
    {
        _orderStatusRepo = orderStatusRepo;
        _userRep = userRep;
        _orderDetailRep = orderDetailRep;
        _productStockPriceRep = productStockPriceRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<ResponseDto> Handle(OrderStatusCreatCommandReq request, CancellationToken cancellationToken)
    {
        ResponseDto response = new();
        if (await _orderStatusRepo.CheckCancelationStatusForOrder(request.OrderStatusDto.OrderId))
        {
            response.IsSuccessed = false;
            response.Message = "ثبت وضعیت به دلیل لغو و یا استرداد سفارش امکان پذیر نمی باشد.";
            return response;
        }

        await _orderStatusRepo.CancelActiveAllOrderStatusesByOrderId(request.OrderStatusDto.OrderId);
        Shared.Models.OrderStatus? orderStatus = _mapper.Map<Shared.Models.OrderStatus>(request.OrderStatusDto);
        orderStatus.IsActive = true;


        if (request.OrderStatusDto.File != null)
        {
            try
            {
                orderStatus.FileName = NameGenerator.GenerateUniqCode() +
                                       Path.GetExtension(request.OrderStatusDto.File.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot/Shared/Files",
                    orderStatus.FileName);
                using (FileStream stream = new(imagePath, FileMode.Create))
                {
                    request.OrderStatusDto.File.CopyTo(stream);
                }
                //File.Delete(tempFile);
            }
            catch (Exception e)
            {
            }
        }


        Shared.Models.OrderStatus orderStatusResult = await _orderStatusRepo.AddAsync(orderStatus);
        response.Object = _mapper.Map<OrderStatusDto>(orderStatusResult);
        Shared.Models.User? createBy = await _userRep.GetUserByUserName(orderStatusResult.CreateBy);
        switch (orderStatusResult.StatusId)
        {
            case 6:

                List<Shared.Models.OrderDetail> orderDetails =
                    await _orderDetailRep.GetOrderDetailsByOrderId(request.OrderStatusDto.OrderId);
                foreach (Shared.Models.OrderDetail item in orderDetails)
                {
                    Shared.Models.ProductStockPrice? productStockPrice =
                        await _productStockPriceRep.GetAsync(item.ProductStockPriceId);
                    ProductRepresentationDto ProductRepDto = new();
                    ProductRepDto.ProductStockPriceId = item.ProductStockPriceId;
                    ProductRepDto.Quantity = item.Count;
                    ProductRepDto.RepresntationId = productStockPrice.RepId;
                    ProductRepDto.TypeId = 3;
                    ProductRepDto.UserId = createBy.Id;

                    await _mediator.Send(new ProductRepresesntationCreateCommandReq(ProductRepDto));
                }


                break;
            case 9:
                List<Shared.Models.OrderDetail> orderDetails2 =
                    await _orderDetailRep.GetOrderDetailsByOrderId(request.OrderStatusDto.OrderId);
                foreach (Shared.Models.OrderDetail item in orderDetails2)
                {
                    Shared.Models.ProductStockPrice? productStockPrice =
                        await _productStockPriceRep.GetAsync(item.ProductStockPriceId);
                    ProductRepresentationDto ProductRepDto = new();
                    ProductRepDto.ProductStockPriceId = item.ProductStockPriceId;
                    ProductRepDto.Quantity = item.Count;
                    ProductRepDto.RepresntationId = productStockPrice.RepId;
                    ProductRepDto.TypeId = 3;
                    ProductRepDto.UserId = createBy.Id;

                    await _mediator.Send(new ProductRepresesntationCreateCommandReq(ProductRepDto));
                }

                break;
            default: break;
        }

        response.IsSuccessed = true;
        response.Message = "عملیات باموفقیت انجا شد";
        return response;
    }
}