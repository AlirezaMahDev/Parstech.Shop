using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRelated.Requests.Queries;

public record GetRelatedProductsByProductIdQueryReq(int productId, string userName) : IRequest<List<ProductDto>>;