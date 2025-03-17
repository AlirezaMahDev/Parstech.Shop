using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Handlers.Queries;

public class RefreshParentQuantityQueryHandler : IRequestHandler<RefreshParentQuantityQueryReq, bool>
{
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly IProductRepository _productRep;

    public RefreshParentQuantityQueryHandler(IProductStockPriceRepository productStockPriceRep,
        IProductRepository productRep)
    {
        _productStockPriceRep = productStockPriceRep;
        _productRep = productRep;
    }

    public async Task<bool> Handle(RefreshParentQuantityQueryReq request, CancellationToken cancellationToken)
    {
        //var productStock = await _productStockPriceRep.GetAsync(request.id);
        Domain.Models.Product? product = await _productRep.GetAsync(request.id);

        if (product.ParentId == null)
        {
            return true;
        }

        Domain.Models.Product? parrent = await _productRep.GetAsync(product.ParentId.Value);
        List<Domain.Models.Product>? childs = await _productRep.GetProductsByParrentId(parrent.Id);
        Domain.Models.ProductStockPrice? parrentStock =
            await _productStockPriceRep.GetProductStockByProductIdAndStoreId(parrent.Id, request.storeId);
        List<int> Quantities = new();

        if (childs.Count > 0)
        {
            switch (product.TypeId)
            {
                case 3:
                    int count = 0;
                    foreach (Domain.Models.Product child in childs)
                    {
                        Domain.Models.ProductStockPrice? pStock =
                            await _productStockPriceRep.GetProductStockByProductIdAndStoreId(child.Id, request.storeId);
                        if (pStock != null)
                        {
                            count += pStock.Quantity;
                        }
                    }

                    parrentStock.Quantity = count;

                    break;
                case 5:
                    foreach (Domain.Models.Product child in childs)
                    {
                        Domain.Models.ProductStockPrice? pStock =
                            await _productStockPriceRep.GetProductStockByProductIdAndStoreId(child.Id, request.storeId);
                        //pStock.QuantityPerBundle = request.QuantityPerBundle;

                        if (pStock != null)
                        {
                            int? quantity = pStock.Quantity / pStock.QuantityPerBundle;
                            if (quantity == null)
                            {
                                Quantities.Add(0);
                            }
                            else
                            {
                                Quantities.Add(quantity.Value);
                            }
                        }
                        else
                        {
                            Quantities.Add(0);
                        }
                    }

                    parrentStock.Quantity = Quantities.Min();
                    break;
            }
        }
        else
        {
            parrentStock.Quantity = 0;
        }

        await _productStockPriceRep.UpdateAsync(parrentStock);
        return true;
    }
}