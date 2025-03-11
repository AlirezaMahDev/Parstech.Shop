using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.Order.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{
	public class OrderWordFileQueryHandler : IRequestHandler<OrderWordFileQueryReq, string>
	{
		private readonly IOrderRepository _orderRepository;

		public OrderWordFileQueryHandler(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}
		public async Task<string> Handle(OrderWordFileQueryReq request, CancellationToken cancellationToken)
		{
			return _orderRepository.GenerateWordOfOrder(request.OrderDetailShowDto);
		}
	}
}
