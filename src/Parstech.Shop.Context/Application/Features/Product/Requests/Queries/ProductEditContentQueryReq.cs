using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record ProductEditContentQueryReq(int productId, string content) : IRequest<ProductDto>;