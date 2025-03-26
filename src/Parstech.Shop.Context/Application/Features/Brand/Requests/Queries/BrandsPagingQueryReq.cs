using MediatR;

using Parstech.Shop.Context.Application.DTOs.Paging;

namespace Parstech.Shop.Context.Application.Features.Brand.Requests.Queries;

public record BrandsPagingQueryReq(ParameterDto Parameter) : IRequest<PagingDto>;