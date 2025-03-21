﻿using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Commands;

public record UpdateCouponCommandReq(CouponDto CouponDto) : IRequest<CouponDto>;