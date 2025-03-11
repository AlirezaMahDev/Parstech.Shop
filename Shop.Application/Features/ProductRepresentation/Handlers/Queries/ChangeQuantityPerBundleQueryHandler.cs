using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using Shop.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductRepresentation.Handlers.Queries
{
    public class ChangeQuantityPerBundleQueryHandler : IRequestHandler<ChangeQuantityPerBundleQueryReq, bool>
    {
        private readonly IProductStockPriceRepository _productStockPriceRep;
        private readonly IProductRepository _productRep;
        private readonly IMediator _mediator;
        public ChangeQuantityPerBundleQueryHandler(IProductStockPriceRepository productStockPriceRep,
            IProductRepository productRep, IMediator mediator)
        {
            _productStockPriceRep = productStockPriceRep;
            _productRep = productRep;
            _mediator = mediator;

        }
        public async Task<bool> Handle(ChangeQuantityPerBundleQueryReq request, CancellationToken cancellationToken)
        {
            var productStock = await _productStockPriceRep.GetAsync(request.productStockPriceId);
            productStock.QuantityPerBundle = request.QuantityPerBundle;
            await _productStockPriceRep.UpdateAsync(productStock);
            await _mediator.Send(new RefreshParentQuantityQueryReq( productStock.ProductId,productStock.StoreId));

            return true;
        }
    }
}
