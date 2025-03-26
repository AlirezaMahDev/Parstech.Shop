using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.ProductRelated.Requests.Queries;

public record GetRelatedProductsByProductIdQueryReq(int productId,string userName):IRequest<List<ProductDto>>;