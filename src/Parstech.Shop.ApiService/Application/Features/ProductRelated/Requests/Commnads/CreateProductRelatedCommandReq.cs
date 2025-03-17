using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRelated.Requests.Commnads;

public record CreateProductRelatedCommandReq(ProductRelatedDto productRelatedDto) : IRequest;