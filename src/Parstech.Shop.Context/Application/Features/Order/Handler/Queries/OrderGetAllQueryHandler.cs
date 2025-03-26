using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.Features.Order.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Queries;

public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQueryReq, List<OrderDto>>
{
    private readonly IOrderRepository _orderRep;
    private readonly IMapper _mapper;

    public OrderGetAllQueryHandler(IOrderRepository orderRep, IMapper mapper)
    {
        _orderRep = orderRep;
        _mapper = mapper;
    }
    public async Task<List<OrderDto>> Handle(OrderGetAllQueryReq request, CancellationToken cancellationToken)
    {
        var list = await _orderRep.GetAll();
        return _mapper.Map<List<OrderDto>>(list);
    }
}