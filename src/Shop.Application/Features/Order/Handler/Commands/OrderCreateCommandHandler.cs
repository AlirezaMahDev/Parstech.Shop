using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.Features.Order.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Commands
{
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

}
