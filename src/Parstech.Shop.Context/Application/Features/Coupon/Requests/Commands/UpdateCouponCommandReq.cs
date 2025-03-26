using MediatR;

using Parstech.Shop.Context.Application.DTOs.Coupon;

namespace Parstech.Shop.Context.Application.Features.Coupon.Requests.Commands;

public record UpdateCouponCommandReq(CouponDto CouponDto):IRequest<CouponDto>;