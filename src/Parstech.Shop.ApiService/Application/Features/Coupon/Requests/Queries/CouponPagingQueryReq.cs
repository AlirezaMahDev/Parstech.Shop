using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;

public record CouponPagingQueryReq(ParameterDto parameter) : IRequest<PagingDto>;