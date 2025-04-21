using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.Order.Requests.Commands;
using Shop.Application.Features.OrderDetail.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Handler.Commands
{
    public class OrderDetailReadCommandHandler : IRequestHandler<OrderDetailReadCommandReq, OrderDetailDto>
    {
        private IOrderDetailRepository _orderDetailRep;
        private IMapper _mapper;
        private IMediator _mediator;

        public OrderDetailReadCommandHandler(IOrderDetailRepository orderDetailRep, IMapper mapper, IMediator mediator)
        {
            _orderDetailRep= orderDetailRep;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<OrderDetailDto> Handle(OrderDetailReadCommandReq request, CancellationToken cancellationToken)
        {
            var item = await _orderDetailRep.GetAsync(request.id);
            return _mapper.Map<OrderDetailDto>(item);
        }
    }
}
