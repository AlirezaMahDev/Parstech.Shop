using MediatR;

using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Handlers.Commands;

public class ProductStockPriceDeleteCommandHandler : IRequestHandler<ProductStockPriceDeleteCommandReq, Unit>
{
    public Task<Unit> Handle(ProductStockPriceDeleteCommandReq request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}