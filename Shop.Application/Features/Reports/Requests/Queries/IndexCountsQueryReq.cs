using MediatR;
using Shop.Application.DTOs.Reports;
using Shop.Application.DTOs.CouponType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Counts.Requests.Queries
{
    public record IndexCountsQueryReq() : IRequest<IndexCountsDto>;

}
