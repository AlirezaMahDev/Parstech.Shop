using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Commands;

public record CreateCouponCommandReq(CouponDto CouponDto) : IRequest<CouponDto>;