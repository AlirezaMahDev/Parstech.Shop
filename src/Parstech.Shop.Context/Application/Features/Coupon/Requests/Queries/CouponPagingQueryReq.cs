using MediatR;

using Parstech.Shop.Context.Application.DTOs.Paging;

namespace Parstech.Shop.Context.Application.Features.Coupon.Requests.Queries;

public record CouponPagingQueryReq(ParameterDto parameter): IRequest<PagingDto>;