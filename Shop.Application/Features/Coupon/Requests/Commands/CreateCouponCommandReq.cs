using MediatR;
using Shop.Application.DTOs.Coupon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Requests.Commands
{
    public record CreateCouponCommandReq(CouponDto CouponDto) : IRequest<CouponDto>;
}
