using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.OrderDetail.Requests.Commands;
using Shop.Application.Features.Product.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Handler.Commands
{

	public class OrderDetailUpdateCommandHandler : IRequestHandler<OrderDetailUpdateCommandReq, OrderDetailDto>
	{

		private IOrderDetailRepository _orderDetailRep;
		private IMapper _mapper;
		

        public OrderDetailUpdateCommandHandler(IOrderDetailRepository orderDetailRep,
			IMapper mapper)
        {
            _orderDetailRep = orderDetailRep;
			_mapper = mapper;	
        }

        public async Task<OrderDetailDto> Handle(OrderDetailUpdateCommandReq request, CancellationToken cancellationToken)
		{
			var item = _mapper.Map<Domain.Models.OrderDetail>(request.OrderDetailDto);
			
			var Result = await _orderDetailRep.UpdateAsync(item);
			return _mapper.Map<OrderDetailDto>(Result);
		}
	}
}
