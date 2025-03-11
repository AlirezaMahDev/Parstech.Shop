using MediatR;
using Shop.Application.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Coupon.Requests.Queries
{
	public record CouponPagingQueryReq(ParameterDto parameter): IRequest<PagingDto>;
}
