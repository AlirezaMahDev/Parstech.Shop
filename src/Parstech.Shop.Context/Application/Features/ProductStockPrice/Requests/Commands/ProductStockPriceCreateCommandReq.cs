using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;

public record ProductStockPriceCreateCommandReq(ProductStockPriceDto ProductStockPriceDto) : IRequest<ProductStockPriceDto>;