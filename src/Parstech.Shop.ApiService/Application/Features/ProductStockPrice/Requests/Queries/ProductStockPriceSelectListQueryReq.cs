using MediatR;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

public record ProductStockPriceSelectListQueryReq(int repId) : IRequest<List<ProductSelectDto>>;