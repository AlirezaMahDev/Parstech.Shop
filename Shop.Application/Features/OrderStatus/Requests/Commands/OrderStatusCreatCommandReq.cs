using MediatR;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.OrderStatus;
using Shop.Application.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderStatus.Requests.Commands
{
	public record OrderStatusCreatCommandReq(OrderStatusDto OrderStatusDto) : IRequest<ResponseDto>;
}
