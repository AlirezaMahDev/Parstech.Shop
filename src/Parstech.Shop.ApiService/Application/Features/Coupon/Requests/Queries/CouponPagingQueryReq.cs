using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Coupon.Requests.Queries;

public record CouponPagingQueryReq(ParameterDto parameter) : IRequest<PagingDto>;