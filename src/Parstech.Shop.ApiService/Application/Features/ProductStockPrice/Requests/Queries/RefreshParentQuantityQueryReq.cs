using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

public record RefreshParentQuantityQueryReq(int id, int storeId) : IRequest<bool>;