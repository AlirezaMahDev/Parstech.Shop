using MediatR;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductLog.Requests.Queries;

public record PriceConflictsCreateLogQueryReq(
    string userName,
    ProductStockPriceDto crrentProduct,
    ProductStockPriceDto EditProduct) : IRequest<Unit>;