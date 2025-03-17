using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

public record ProductStockPriceEditPriceQueryReq(ProductDto product) : IRequest<ProductStockPriceDto>;