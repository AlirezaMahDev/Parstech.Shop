using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;

public record CheckAndUseCouponQueryReq(
    string orderORdetail,
    OrderDto orderDto,
    OrderDetailDto orderDetailDto,
    string CouponCode) : IRequest<OrderResponse>;