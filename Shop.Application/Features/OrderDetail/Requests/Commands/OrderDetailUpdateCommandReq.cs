using MediatR;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Requests.Commands
{
	public record OrderDetailUpdateCommandReq(OrderDetailDto OrderDetailDto) : IRequest<OrderDetailDto>;

}
