using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Brand.Requests.Queries;

public record BrandsPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;