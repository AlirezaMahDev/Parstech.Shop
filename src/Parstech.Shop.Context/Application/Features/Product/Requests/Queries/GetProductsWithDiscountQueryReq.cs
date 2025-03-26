using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record GetProductsWithDiscountQueryReq(int take,int sectionId) : IRequest<List<ProductDto>>;