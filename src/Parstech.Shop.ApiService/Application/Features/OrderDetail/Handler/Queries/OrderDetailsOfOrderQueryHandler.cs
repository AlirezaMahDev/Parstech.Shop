using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Handler.Queries
{
    public class OrderDetailsOfOrderQueryHandler : IRequestHandler<OrderDetailsOfOrderQueryReq, List<OrderDetailDto>>
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OrderDetailsOfOrderQueryHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper, IMediator mediator)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<List<OrderDetailDto>> Handle(OrderDetailsOfOrderQueryReq request, CancellationToken cancellationToken)
        {
            var orderDetails = await _orderDetailRepository.GetOrderDetailsByOrderId(request.orderId);
            return _mapper.Map<List<OrderDetailDto>>(orderDetails);
        }
    }
}
