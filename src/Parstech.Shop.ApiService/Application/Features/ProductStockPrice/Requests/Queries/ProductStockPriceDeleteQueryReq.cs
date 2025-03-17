using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

public record ProductStockPriceDeleteQueryReq(int repId, int productStockPriceId) : IRequest<bool>;