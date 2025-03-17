using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.OrderShipping.Request.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderShipping.Handler.Queries;

public class
    OrderShippingGetByOrderIdQueryHandler : IRequestHandler<OrderShippingGetByOrderIdQueryReq, OrderShippingDto>
{
    private readonly IOrderShippingRepository _orderShippingRep;
    private readonly IMapper _mapper;

    public OrderShippingGetByOrderIdQueryHandler(IOrderShippingRepository orderShippingRep, IMapper mapper)
    {
        _orderShippingRep = orderShippingRep;
        _mapper = mapper;
    }

    public async Task<OrderShippingDto> Handle(OrderShippingGetByOrderIdQueryReq request,
        CancellationToken cancellationToken)
    {
        OrderShippingDto orderShippingDto = new();
        Shared.Models.OrderShipping item = await _orderShippingRep.GetOrderShippingByOrderId(request.orderId);
        if (item.Id == 0)
        {
            return orderShippingDto;
        }

        return _mapper.Map<OrderShippingDto>(item);
    }
}