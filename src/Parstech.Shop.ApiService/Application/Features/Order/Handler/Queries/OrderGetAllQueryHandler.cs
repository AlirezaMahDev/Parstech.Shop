using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

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
        IReadOnlyList<Shared.Models.Order> list = await _orderRep.GetAll();
        return _mapper.Map<List<OrderDto>>(list);
    }
}