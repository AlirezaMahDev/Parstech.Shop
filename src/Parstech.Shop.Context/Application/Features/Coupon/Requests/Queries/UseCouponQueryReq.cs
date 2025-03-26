using MediatR;

using Parstech.Shop.Context.Application.DTOs.Order;

namespace Parstech.Shop.Context.Application.Features.Coupon.Requests.Queries;

public record UseCouponQueryReq(int orderId,string coupon):IRequest<OrderResponse>;