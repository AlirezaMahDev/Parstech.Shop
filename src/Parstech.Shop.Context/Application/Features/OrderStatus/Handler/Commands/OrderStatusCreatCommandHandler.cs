using AutoMapper;
using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.OrderStatus;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.Context.Application.Generator;

namespace Parstech.Shop.Context.Application.Features.OrderStatus.Handler.Commands;

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
        _userRep=userRep;
        _orderDetailRep = orderDetailRep;
        _productStockPriceRep= productStockPriceRep;
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
        var orderStatus = _mapper.Map<Domain.Models.OrderStatus>(request.OrderStatusDto);
        orderStatus.IsActive = true;


        if (request.OrderStatusDto.File != null)
        {
            try
            {
                orderStatus.FileName = NameGenerator.GenerateUniqCode() + Path.GetExtension(request.OrderStatusDto.File.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Shared/Files", orderStatus.FileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    request.OrderStatusDto.File.CopyTo(stream);
                }
                //File.Delete(tempFile);
            }
            catch (Exception e)
            {
            }
        }
            

        var orderStatusResult = await _orderStatusRepo.AddAsync(orderStatus);
        response.Object=_mapper.Map<OrderStatusDto>(orderStatusResult);
        var createBy=await _userRep.GetUserByUserName(orderStatusResult.CreateBy);
        switch (orderStatusResult.StatusId)
        {
            case 6:
                
                var orderDetails=await _orderDetailRep.GetOrderDetailsByOrderId(request.OrderStatusDto.OrderId); 
                foreach(var item in orderDetails)
                {
                    var productStockPrice =await _productStockPriceRep.GetAsync(item.ProductStockPriceId);
                    ProductRepresentationDto ProductRepDto = new();
                    ProductRepDto.ProductStockPriceId = item.ProductStockPriceId;
                    ProductRepDto.Quantity = item.Count;
                    ProductRepDto.RepresntationId = productStockPrice.RepId;
                    ProductRepDto.TypeId = 3;
                    ProductRepDto.UserId=createBy.Id;

                    await _mediator.Send(new ProductRepresesntationCreateCommandReq(ProductRepDto));
                }
                    

                break;
            case 9:
                var orderDetails2 = await _orderDetailRep.GetOrderDetailsByOrderId(request.OrderStatusDto.OrderId);
                foreach (var item in orderDetails2)
                {
                    var productStockPrice = await _productStockPriceRep.GetAsync(item.ProductStockPriceId);
                    ProductRepresentationDto ProductRepDto = new();
                    ProductRepDto.ProductStockPriceId = item.ProductStockPriceId;
                    ProductRepDto.Quantity = item.Count;
                    ProductRepDto.RepresntationId = productStockPrice.RepId;
                    ProductRepDto.TypeId = 3;
                    ProductRepDto.UserId=createBy.Id;

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