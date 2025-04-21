using MediatR;
using Shop.Application.DTOs.Coupon;
using Shop.Application.DTOs.CouponType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Requests.Commands
{
    public record CouponGetByIdCommandReq(int couponId) : IRequest<CouponDto>;
}
