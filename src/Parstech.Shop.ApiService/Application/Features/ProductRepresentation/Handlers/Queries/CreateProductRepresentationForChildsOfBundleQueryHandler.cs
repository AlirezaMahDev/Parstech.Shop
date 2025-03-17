using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Requests.Queries;
using Parstech.Shop.ApiService.Application.Generator;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.ProductRepresentation.Handlers.Queries;

internal class
    CreateProductRepresentationForChildsOfBundleQueryHandler : IRequestHandler<
    CreateProductRepresentationForChildsOfBundleQueryReq>
{
    private readonly IMediator _mediator;
    private readonly IProductStockPriceRepository _productStockPriceRep;
    private readonly IProductRepository _productRep;

    public CreateProductRepresentationForChildsOfBundleQueryHandler(IMediator mediator,
        IProductStockPriceRepository productStockPriceRep,
        IProductRepository productRep)
    {
        _mediator = mediator;
        _productStockPriceRep = productStockPriceRep;
        _productRep = productRep;
    }

    public async Task Handle(CreateProductRepresentationForChildsOfBundleQueryReq request,
        CancellationToken cancellationToken)
    {
        Shared.Models.ProductStockPrice? productStock = await _productStockPriceRep.GetAsync(request.productStockId);
        Shared.Models.Product? product = await _productRep.GetAsync(productStock.ProductId);

        List<Shared.Models.Product> childs = await _productRep.GetProductsByParrentId(product.Id);

        List<int> Quantities = new();
        foreach (Shared.Models.Product child in childs)
        {
            ProductRepresentationDto ProductRep = new() { UserId = request.userId, TypeId = 2 };
            Shared.Models.ProductStockPrice? pStock =
                await _productStockPriceRep.GetProductStockByProductIdAndStoreId(child.Id, productStock.StoreId);
            int? quantity = pStock.QuantityPerBundle * request.orderDetailCount;
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