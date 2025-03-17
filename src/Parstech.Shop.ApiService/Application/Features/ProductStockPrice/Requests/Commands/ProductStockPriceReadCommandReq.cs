using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;

public record ProductStockPriceReadCommandReq(int id) : IRequest<ProductStockPriceDto>;