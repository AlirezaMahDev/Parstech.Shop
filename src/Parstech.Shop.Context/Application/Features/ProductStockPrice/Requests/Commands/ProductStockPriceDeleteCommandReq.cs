using MediatR;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;

public record ProductStockPriceDeleteCommandReq(int id) : IRequest<Unit>;