using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.Features.Order.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Commands;

public class OrderReadCommandHandler : IRequestHandler<OrderReadCommandReq, OrderDto>
{
    private IOrderRepository _orderRepository;
    private IMapper _mapper;
    private IMediator _mediator;

    public OrderReadCommandHandler(IOrderRepository orderRepository, IMapper mapper, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<OrderDto> Handle(OrderReadCommandReq request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.id);
        return _mapper.Map<OrderDto>(order);
    }
}