using MediatR;

using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Application.DTOs.OrderDetail;

namespace Parstech.Shop.Context.Application.Features.Coupon.Requests.Queries;

public record CheckAndUseCouponQueryReq(string orderORdetail,OrderDto orderDto,OrderDetailDto orderDetailDto,string CouponCode):IRequest<OrderResponse>;