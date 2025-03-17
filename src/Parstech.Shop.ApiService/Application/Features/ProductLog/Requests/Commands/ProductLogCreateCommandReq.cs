using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductLog.Requests.Commands;

public record ProductLogCreateCommandReq(
    int typeId,
    string userName,
    string oldValue,
    string newValue,
    int productStockPriceId) : IRequest<Unit>;