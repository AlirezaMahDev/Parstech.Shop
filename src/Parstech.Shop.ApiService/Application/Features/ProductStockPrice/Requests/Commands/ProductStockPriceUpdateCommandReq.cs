using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;

public record ProductStockPriceUpdateCommandReq(ProductStockPriceDto ProductStockPriceDto)
    : IRequest<ProductStockPriceDto>;