using AutoMapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Commands;

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
        Domain.Models.Order? order = await _orderRepository.GetAsync(request.id);
        return _mapper.Map<OrderDto>(order);
    }
}