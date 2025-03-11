using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Requests.Queries
{
	public record OrdersFilterDataQueryReq(string userName):IRequest<OrderFilterDto>;

}
