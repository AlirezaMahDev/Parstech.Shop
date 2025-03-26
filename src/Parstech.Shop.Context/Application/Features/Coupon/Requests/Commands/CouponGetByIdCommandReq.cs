using MediatR;

using Parstech.Shop.Context.Application.DTOs.Coupon;

namespace Parstech.Shop.Context.Application.Features.Coupon.Requests.Commands;

public record CouponGetByIdCommandReq(int couponId) : IRequest<CouponDto>;