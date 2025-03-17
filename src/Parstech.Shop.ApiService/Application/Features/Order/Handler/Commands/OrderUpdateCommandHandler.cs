using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Commands;

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
        Shared.Models.Order? order = _mapper.Map<Shared.Models.Order>(request.OrderDto);
        Shared.Models.Order orderResult = await _orderRepository.UpdateAsync(order);
        return _mapper.Map<OrderDto>(orderResult);
    }
}