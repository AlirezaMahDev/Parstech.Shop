using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPrice.Handlers.Queries
{

    public class RefreshParentQuantityQueryHandler : IRequestHandler<RefreshParentQuantityQueryReq, bool>
    {
        private readonly IProductStockPriceRepository _productStockPriceRep;
        private readonly IProductRepository _productRep;
        public RefreshParentQuantityQueryHandler(IProductStockPriceRepository productStockPriceRep, IProductRepository productRep)
        {
            _productStockPriceRep = productStockPriceRep;
            _productRep = productRep;

        }
        public async Task<bool> Handle(RefreshParentQuantityQueryReq request, CancellationToken cancellationToken)
        {
            //var productStock = await _productStockPriceRep.GetAsync(request.id);
            var product = await _productRep.GetAsync(request.id);

            if (product.ParentId == null)
            {
                return true;
            }

            var parrent = await _productRep.GetAsync(product.ParentId.Value);
            var childs = await _productRep.GetProductsByParrentId(parrent.Id);
            var parrentStock = await _productStockPriceRep.GetProductStockByProductIdAndStoreId(parrent.Id, request.storeId);
            List<int> Quantities = new List<int>();

            if (childs.Count > 0)
            {

                switch (product.TypeId)
                {
                    case 3:
                        var count = 0;
                        foreach (var child in childs)
                        {
                            var pStock = await _productStockPriceRep.GetProductStockByProductIdAndStoreId(child.Id, request.storeId);
                            if(pStock != null)
                            {
                                count += pStock.Quantity;
                            }
                            
                        }

                        parrentStock.Quantity = count;

                        break;
                    case 5:
                        foreach (var child in childs)
                        {
                            var pStock = await _productStockPriceRep.GetProductStockByProductIdAndStoreId(child.Id, request.storeId);
                            //pStock.QuantityPerBundle = request.QuantityPerBundle;

                            if (pStock != null)
                            {
                                var quantity = pStock.Quantity / pStock.QuantityPerBundle;
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
}
