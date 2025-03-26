using MediatR;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Property;

namespace Parstech.Shop.Context.Application.Features.Property.Requests.Queries;

public record PropertyPagingQueryReq(PropertyParameterDto Parameter) : IRequest<PagingDto>;