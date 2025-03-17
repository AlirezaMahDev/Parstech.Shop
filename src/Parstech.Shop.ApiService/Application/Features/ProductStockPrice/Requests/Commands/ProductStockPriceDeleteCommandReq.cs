using MediatR;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;

public record ProductStockPriceDeleteCommandReq(int id) : IRequest<Unit>;