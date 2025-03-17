using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Commands;

public record CreateCouponCommandReq(CouponDto CouponDto) : IRequest<CouponDto>;