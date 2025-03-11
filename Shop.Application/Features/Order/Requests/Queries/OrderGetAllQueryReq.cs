using MediatR;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Requests.Queries
{
	public record OrderGetAllQueryReq() : IRequest<List<OrderDto>>;
}
