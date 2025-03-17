using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

public record GetProductStockPriceStoreOfProductQueryReq(int ProductId) : IRequest<List<ProductStockPriceStoreDto>>;