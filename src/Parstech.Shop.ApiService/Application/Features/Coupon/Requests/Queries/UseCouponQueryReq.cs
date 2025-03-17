using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;

public record UseCouponQueryReq(int orderId, string coupon) : IRequest<OrderResponse>;