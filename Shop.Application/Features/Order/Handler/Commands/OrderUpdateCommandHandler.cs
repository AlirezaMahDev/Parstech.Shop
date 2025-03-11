using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Order.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Commands
{
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
}
