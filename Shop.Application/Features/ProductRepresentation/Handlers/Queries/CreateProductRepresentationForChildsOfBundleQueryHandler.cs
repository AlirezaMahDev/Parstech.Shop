using MediatR;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.Features.ProductRepresentation.Requests.Commands;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;
using Shop.Application.Generator;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductRepresentation.Handlers.Queries
{
    internal class CreateProductRepresentationForChildsOfBundleQueryHandler : IRequestHandler<CreateProductRepresentationForChildsOfBundleQueryReq>
    {
        private readonly IMediator _mediator;
        private readonly IProductStockPriceRepository _productStockPriceRep;
        private readonly IProductRepository _productRep;
        public CreateProductRepresentationForChildsOfBundleQueryHandler(IMediator mediator,
            IProductStockPriceRepository productStockPriceRep, IProductRepository productRep)
        {
            _mediator = mediator;
            _productStockPriceRep = productStockPriceRep;
            _productRep = productRep;
        }
        public async Task Handle(CreateProductRepresentationForChildsOfBundleQueryReq request, CancellationToken cancellationToken)
        {
            var productStock = await _productStockPriceRep.GetAsync(request.productStockId);
            var product = await _productRep.GetAsync(productStock.ProductId);
            
            var childs = await _productRep.GetProductsByParrentId(product.Id);

            List<int> Quantities = new List<int>();
            foreach (var child in childs)
            {
                ProductRepresentationDto ProductRep = new ProductRepresentationDto()
                {
                    UserId = request.userId,
                    TypeId = 2,
                };
                var pStock = await _productStockPriceRep.GetProductStockByProductIdAndStoreId(child.Id, productStock.StoreId);
                var quantity = pStock.QuantityPerBundle * request.orderDetailCount;
                if (quantity == null)
                {
                    quantity = request.orderDetailCount;
                }
                ProductRep.UniqeCode = CodeGenerator.GenerateUniqCode();
                ProductRep.ProductStockPriceId = pStock.Id;
                ProductRep.RepresntationId = pStock.RepId;
                ProductRep.Quantity = quantity.Value;
                await _mediator.Send(new ProductRepresesntationCreateCommandReq(ProductRep));

            }
            
        }
    }
}
