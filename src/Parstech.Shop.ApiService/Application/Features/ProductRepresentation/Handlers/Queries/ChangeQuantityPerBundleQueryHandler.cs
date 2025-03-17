using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Handlers.Queries;

public class ChangeQuantityPerBundleQueryHandler : IRequestHandler<ChangeQuantityPerBundleQueryReq, bool>
{
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly IProductRepository _productRep;
    private readonly IMediator _mediator;

    public ChangeQuantityPerBundleQueryHandler(IProductStockPriceRepository productStockPriceRep,
        IProductRepository productRep,
        IMediator mediator)
    {
        _productStockPriceRep = productStockPriceRep;
        _productRep = productRep;
        _mediator = mediator;
    }

    public async Task<bool> Handle(ChangeQuantityPerBundleQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.ProductStockPrice? productStock =
            await _productStockPriceRep.GetAsync(request.productStockPriceId);
        productStock.QuantityPerBundle = request.QuantityPerBundle;
        await _productStockPriceRep.UpdateAsync(productStock);
        await _mediator.Send(new RefreshParentQuantityQueryReq(productStock.ProductId, productStock.StoreId));

        return true;
    }
}