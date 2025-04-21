using AutoMapper;
using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Order;
using Shop.Application.Features.Categury.Requests.Queries;
using Shop.Application.Features.Order.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{
	public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQueryReq, List<OrderDto>>
	{
		private readonly IOrderRepository _orderRep;
		private readonly IMapper _mapper;

		public OrderGetAllQueryHandler(IOrderRepository orderRep, IMapper mapper)
		{
			_orderRep = orderRep;
			_mapper = mapper;
		}
		public async Task<List<OrderDto>> Handle(OrderGetAllQueryReq request, CancellationToken cancellationToken)
		{
			var list = await _orderRep.GetAll();
			return _mapper.Map<List<OrderDto>>(list);
		}
	}
}
