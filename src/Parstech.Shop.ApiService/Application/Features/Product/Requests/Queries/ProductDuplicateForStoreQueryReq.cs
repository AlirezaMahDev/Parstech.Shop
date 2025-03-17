using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

public record ProductDuplicateForStoreQueryReq(int productId, int storeId) : IRequest<bool>;

public record ProductDuplicateQueryReq(int productId) : IRequest<Unit>;