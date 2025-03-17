using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;

public record ChangeQuantityPerBundleQueryReq(int productStockPriceId, int QuantityPerBundle) : IRequest<bool>;