using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;

public record ProductPresentationsWithProductPagingQueryReq(ProductRepresenationParameterDto Parameter)
    : IRequest<PagingDto>;