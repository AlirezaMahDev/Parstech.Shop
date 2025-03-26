using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record ProductQuickEditQueryReq(ProductQuickEditDto ProductQuickEditDto) : IRequest<ProductDto>;