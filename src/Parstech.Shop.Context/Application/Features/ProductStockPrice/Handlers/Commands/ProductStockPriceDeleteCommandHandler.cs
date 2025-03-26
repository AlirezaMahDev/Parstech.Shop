using MediatR;

using Parstech.Shop.Context.Application.Features.ProductStockPrice.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.ProductStockPrice.Handlers.Commands;

public class ProductStockPriceDeleteCommandHandler : IRequestHandler<ProductStockPriceDeleteCommandReq, Unit>
{
    public Task<Unit> Handle(ProductStockPriceDeleteCommandReq request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}