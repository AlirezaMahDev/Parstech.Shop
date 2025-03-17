using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Commands;

public record ProductRepresesntationCreateCommandReq(ProductRepresentationDto ProductRepresentationDto)
    : IRequest<ProductRepresentationDto>;

public record ProductRepresesntationQuickCreateCommandReq(ProductRepresentationDto ProductRepresentationDto)
    : IRequest<ProductRepresentationDto>;