using MediatR;

namespace Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

public record ProductDuplicateForStoreQueryReq(int productId,int storeId):IRequest<bool>;
public record ProductDuplicateQueryReq(int productId) : IRequest<Unit>;