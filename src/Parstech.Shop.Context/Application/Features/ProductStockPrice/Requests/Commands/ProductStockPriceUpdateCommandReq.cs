using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;

public record ProductStockPriceUpdateCommandReq(ProductStockPriceDto ProductStockPriceDto) : IRequest<ProductStockPriceDto>;