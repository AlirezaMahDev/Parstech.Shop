using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.Features.Order.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Commands;

public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommandReq, OrderDto>
{
    private IOrderRepository _orderRepository;
    private IMapper _mapper;
    private IMediator _mediator;

    public OrderUpdateCommandHandler(IOrderRepository orderRepository, IMapper mapper, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<OrderDto> Handle(OrderUpdateCommandReq request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Domain.Models.Order>(request.OrderDto);
        var orderResult = await _orderRepository.UpdateAsync(order);
        return _mapper.Map<OrderDto>(orderResult);
    }
}