using MediatR;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Commands;

public record ProductRepresesntationCreateCommandReq
    (ProductRepresentationDto ProductRepresentationDto) : IRequest<ProductRepresentationDto>;


public record ProductRepresesntationQuickCreateCommandReq(ProductRepresentationDto ProductRepresentationDto) : IRequest<ProductRepresentationDto>;