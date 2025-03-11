using MediatR;
using Shop.Application.DTOs.Coupon;
using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Requests.Queries
{
	public record CheckAndUseCouponQueryReq(string orderORdetail,OrderDto orderDto,OrderDetailDto orderDetailDto,string CouponCode):IRequest<OrderResponse>;

}
