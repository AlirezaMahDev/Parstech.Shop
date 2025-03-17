using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Brand.Requests.Queries;

public record BrandsPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;