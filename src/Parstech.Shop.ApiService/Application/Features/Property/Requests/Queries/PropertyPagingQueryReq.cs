using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Property.Requests.Queries;

public record PropertyPagingQueryReq(PropertyParameterDto Parameter) : IRequest<PagingDto>;