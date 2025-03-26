using MediatR;

using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;

namespace Parstech.Shop.Context.Application.Features.ProductLog.Requests.Queries;

public record PriceConflictsCreateLogQueryReq(string userName, ProductStockPriceDto crrentProduct, ProductStockPriceDto EditProduct) : IRequest<Unit>;