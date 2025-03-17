using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;

public record CreateProductRepresentationForChildsOfBundleQueryReq(
    int userId,
    int productId,
    int productStockId,
    int orderDetailCount) : IRequest;