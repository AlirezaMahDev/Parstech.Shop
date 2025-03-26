using AutoMapper;

using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.Features.Order.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Order.Handler.Commands;

public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommandReq, OrderDto>
{
    private IOrderRepository _orderRepository;
    private IMapper _mapper;
    private IMediator _mediator;

    public OrderCreateCommandHandler(IOrderRepository orderRepository, IMapper mapper, IMediator mediator)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _mediator = mediator;
    }
    public async Task<OrderDto> Handle(OrderCreateCommandReq request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Domain.Models.Order>(request.OrderDto);

        var orderResult = await _orderRepository.AddAsync(order);

        return _mapper.Map<OrderDto>(orderResult);
    }
}