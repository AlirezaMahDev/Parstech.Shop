using MediatR;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderShipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderShipping.Request.Queries
{
	//Type=Refresh Or Change
	public record OrderShippingChangeQueryReq(string Type,int UserShippingId,int OrderId, long OrderSum) : IRequest<long>;
}
