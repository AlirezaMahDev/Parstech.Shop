using MediatR;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;

public record ChangeQuantityPerBundleQueryReq(int productStockPriceId,int QuantityPerBundle):IRequest<bool>;