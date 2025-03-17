using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRelated.Requests.Commnads;

public record CreateProductRelatedCommandReq(ProductRelatedDto productRelatedDto) : IRequest;