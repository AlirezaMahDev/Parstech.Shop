using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.UserShipping.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class NotFinallyOrderOfUserQueryHandler : IRequestHandler<NotFinallyOrderOfUserQueryReq, OrderDetailShowDto>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IUserBillingRepository _userBillingRepository;
    private readonly IUserShippingRepository _userShippingRepository;
    private readonly IOrderShippingRepository _orderShippingRepository;
    private readonly IProductRepository _productRep;

    public NotFinallyOrderOfUserQueryHandler(IMapper mapper,
        IMediator mediator,
        IOrderRepository orderRepository,
        IOrderDetailRepository orderDetailRepository,
        IUserBillingRepository userBillingRepository,
        IUserShippingRepository userShippingRepository,
        IOrderShippingRepository orderShippingRepository,
        IProductRepository productRep)
    {
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _userBillingRepository = userBillingRepository;
        _userShippingRepository = userShippingRepository;
        _orderShippingRepository = orderShippingRepository;
        _productRep = productRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<OrderDetailShowDto> Handle(NotFinallyOrderOfUserQueryReq request,
        CancellationToken cancellationToken)
    {
        OrderDetailShowDto orderDetailShowDto = new();
        orderDetailShowDto.OrderDetailDto = new();
        Domain.Models.Order order = await _orderRepository.GetNotFinallyOrderOfUser(request.userId);
        List<Domain.Models.OrderDetail> orderDetails =
            await _orderDetailRepository.GetOrderDetailsByOrderId(order.OrderId);
        Domain.Models.UserBilling? userBilling = await _userBillingRepository.GetUserBillingByUserId(order.UserId);
        Domain.Models.OrderShipping orderShipping =
            await _orderShippingRepository.GetOrderShippingByOrderId(order.OrderId);
        orderDetailShowDto.OrderShippingId = orderShipping.Id;
        //var userShipping = await _userShippingRepository.GetAsync(orderShipping.UserShippingId);
        orderDetailShowDto.Order = _mapper.Map<OrderDto>(order);
        orderDetailShowDto.Order.CreateDateShamsi = order.CreateDate.ToShamsi();
        foreach (Domain.Models.OrderDetail item in orderDetails)
        {
            OrderDetailDto? dto = _mapper.Map<OrderDetailDto>(item);
            //var product = await _productRep.GetAsync(item.ProductId);
            //dto.ProductName = product.Name;
            //dto.ProductCode = product.Code;

            orderDetailShowDto.OrderDetailDto.Add(dto);
        }

        //orderDetailShowDto.UserShipping = _mapper.Map<UserShippingDto>(userShipping);
        orderDetailShowDto.Costumer = _mapper.Map<UserBillingDto>(userBilling);
        orderDetailShowDto.UserShippingList =
            await _mediator.Send(new UserShippingOfUserQueryReq(orderDetailShowDto.Order.UserId));
        return orderDetailShowDto;
    }
}