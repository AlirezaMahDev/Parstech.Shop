using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class GetOpenOrderOfUserQueryHandler : IRequestHandler<GetOpenOrderOfUserQueryReq, OrderDto>
{
    private readonly IUserRepository _userRep;
    private readonly IOrderRepository _orderRep;
    private readonly IOrderDetailRepository _orderDetailRep;
    private readonly IOrderShippingRepository _orderShippingRep;
    private readonly IOrderPayRepository _orderPayRep;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetOpenOrderOfUserQueryHandler(IUserRepository userRep,
        IOrderRepository orderRep,
        IOrderDetailRepository orderDetailRep,
        IOrderShippingRepository orderShippingRep,
        IOrderPayRepository orderPayRep,
        IMapper mapper,
        IMediator mediator)
    {
        _userRep = userRep;
        _orderRep = orderRep;
        _orderDetailRep = orderDetailRep;
        _orderShippingRep = orderShippingRep;
        _orderPayRep = orderPayRep;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<OrderDto> Handle(GetOpenOrderOfUserQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.User? user = await _userRep.GetUserByUserName(request.userName);
        Shared.Models.Order order = await _orderRep.GetNotFinallyOrderOfUser(user.Id);
        return _mapper.Map<OrderDto>(order);
    }
}