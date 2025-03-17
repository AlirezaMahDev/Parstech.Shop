using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;

public record ProductRepresentaionPagingQueryReq(ProductRepresenationParameterDto ProductRepresenationParameterDto)
    : IRequest<ProductRepresentationPagingDto>;