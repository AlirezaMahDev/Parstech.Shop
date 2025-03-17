using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record GetProductsWithDiscountQueryReq(int take, int sectionId) : IRequest<List<ProductDto>>;