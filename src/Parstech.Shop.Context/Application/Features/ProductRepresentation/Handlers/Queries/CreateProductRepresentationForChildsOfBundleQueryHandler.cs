using MediatR;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.Context.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.Context.Application.Generator;

namespace Parstech.Shop.Context.Application.Features.ProductRepresentation.Handlers.Queries;

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

        List<int> Quantities = new();
        foreach (var child in childs)
        {
            ProductRepresentationDto ProductRep = new()
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