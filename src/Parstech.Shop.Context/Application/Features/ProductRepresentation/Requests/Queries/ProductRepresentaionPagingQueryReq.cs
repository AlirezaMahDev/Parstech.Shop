using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;

public record ProductRepresentaionPagingQueryReq(ProductRepresenationParameterDto ProductRepresenationParameterDto) : IRequest<ProductRepresentationPagingDto>;