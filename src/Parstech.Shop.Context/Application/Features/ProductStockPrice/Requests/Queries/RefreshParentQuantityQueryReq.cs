using MediatR;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Queries;

public record RefreshParentQuantityQueryReq(int id,int storeId):IRequest<bool>;