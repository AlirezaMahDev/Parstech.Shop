using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Requests.Queries
{
	public record UseCouponQueryReq(int orderId,string coupon):IRequest<OrderResponse>;

}
