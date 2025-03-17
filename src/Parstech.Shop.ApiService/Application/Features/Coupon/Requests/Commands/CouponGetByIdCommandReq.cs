using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Commands;

public record CouponGetByIdCommandReq(int couponId) : IRequest<CouponDto>;