using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Property.Requests.Queries;

public record PropertyPagingQueryReq(PropertyParameterDto Parameter) : IRequest<PagingDto>;