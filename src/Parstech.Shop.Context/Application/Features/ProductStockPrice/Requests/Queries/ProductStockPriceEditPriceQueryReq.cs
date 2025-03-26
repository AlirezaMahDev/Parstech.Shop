using MediatR;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

public record ProductStockPriceEditPriceQueryReq(ProductDto product) : IRequest<ProductStockPriceDto>;