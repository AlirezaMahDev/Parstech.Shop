using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

public record ProductStockPriceSelectListQueryReq(int repId) : IRequest<List<ProductSelectDto>>;