using MediatR;
using Shop.Application.DTOs.CouponType;
using Shop.Application.DTOs.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.CouponType.Requests.Commands
{
    public record CouponTypeReadCommandReq() : IRequest<List<CouponTypeDto>>;

    
}
