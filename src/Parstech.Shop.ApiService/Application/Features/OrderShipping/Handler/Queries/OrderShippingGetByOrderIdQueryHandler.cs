using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.OrderShipping;
using Shop.Application.Features.OrderShipping.Request.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderShipping.Handler.Queries
{
    public class OrderShippingGetByOrderIdQueryHandler : IRequestHandler<OrderShippingGetByOrderIdQueryReq, OrderShippingDto>
    {
        private readonly IOrderShippingRepository _orderShippingRep;
        private readonly IMapper _mapper;
        public OrderShippingGetByOrderIdQueryHandler(IOrderShippingRepository orderShippingRep, IMapper mapper)
        {
            _orderShippingRep = orderShippingRep;
            _mapper = mapper;
        }
        public async Task<OrderShippingDto> Handle(OrderShippingGetByOrderIdQueryReq request, CancellationToken cancellationToken)
        {
            OrderShippingDto orderShippingDto=new OrderShippingDto();
            var item =await _orderShippingRep.GetOrderShippingByOrderId(request.orderId);
            if (item.Id == 0)
            {
                return orderShippingDto;
            }
            return _mapper.Map<OrderShippingDto>(item);
        }
    }
}
