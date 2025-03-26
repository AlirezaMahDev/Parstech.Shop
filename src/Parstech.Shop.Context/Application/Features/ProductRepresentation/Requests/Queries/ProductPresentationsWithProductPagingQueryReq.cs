using MediatR;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;

public record ProductPresentationsWithProductPagingQueryReq(ProductRepresenationParameterDto Parameter) : IRequest<PagingDto>;