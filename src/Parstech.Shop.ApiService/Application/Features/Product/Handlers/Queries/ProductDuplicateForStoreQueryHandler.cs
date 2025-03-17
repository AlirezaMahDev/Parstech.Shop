using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class ProductDuplicateForStoreQueryHandler : IRequestHandler<ProductDuplicateForStoreQueryReq, bool>
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _galleryRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly IProductStockPriceRepository _productStockRep;

    public ProductDuplicateForStoreQueryHandler(IMediator mediator,
        IProductRepository productRep,
        IUserStoreRepository userStoreRep,
        IProductPropertyRepository productPropertyRep,
        IProductGallleryRepository galleryRep,
        IProductCateguryRepository productCateguryRep,
        IProductStockPriceRepository productStockRep)
    {
        _mediator = mediator;
        _productRep = productRep;
        _userStoreRep = userStoreRep;
        _productCateguryRep = productCateguryRep;
        _productPropertyRep = productPropertyRep;
        _galleryRep = galleryRep;
        _productStockRep = productStockRep;
    }

    public async Task<bool> Handle(ProductDuplicateForStoreQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Product? product = await _productRep.GetAsync(request.productId);
        Shared.Models.UserStore? store = await _userStoreRep.GetAsync(request.storeId);

        bool existTekrari = await _productStockRep.ExistStockForProductIdAndStore(request.productId, request.storeId);
        if (existTekrari)
        {
            return false;
        }

        ProductStockPriceDto productStock = new()
        {
            ProductId = product.Id,
            RepId = store.RepId,
            StoreId = store.Id,
            Price = 0,
            SalePrice = 0,
            DiscountPrice = 0,
            BasePrice = 0,
            StockStatus = false,
            Quantity = 0,
            MaximumSaleInOrder = 0,
            TaxId = 1
        };
        await _mediator.Send(new ProductStockPriceCreateCommandReq(productStock));

        if (product.TypeId == 2 || product.TypeId == 4)
        {
            List<Shared.Models.Product> childs = await _productRep.GetProductsByParrentId(product.Id);
            if (childs.Count > 0)
            {
                foreach (Shared.Models.Product child in childs)
                {
                    ProductStockPriceDto productStockChild = new()
                    {
                        ProductId = child.Id,
                        RepId = store.RepId,
                        StoreId = store.Id,
                        Price = 0,
                        SalePrice = 0,
                        DiscountPrice = 0,
                        BasePrice = 0,
                        StockStatus = false,
                        Quantity = 0,
                        MaximumSaleInOrder = 0,
                        TaxId = 1
                    };
                    await _mediator.Send(new ProductStockPriceCreateCommandReq(productStockChild));
                }
            }
        }


        if (!await _productStockRep.ExistStockForParentProduct(product.Id))
        {
            ProductStockPriceDto parentProductStock = new()
            {
                ProductId = product.ParentId.Value,
                RepId = store.RepId,
                StoreId = store.Id,
                Price = 0,
                SalePrice = 0,
                DiscountPrice = 0,
                BasePrice = 0,
                StockStatus = false,
                Quantity = 0,
                MaximumSaleInOrder = 0,
                TaxId = 1
            };
            await _mediator.Send(new ProductStockPriceCreateCommandReq(parentProductStock));
        }

        return true;
    }
}

public class ProductDuplicateQueryHandler : IRequestHandler<ProductDuplicateQueryReq, Unit>
{
    private readonly IMediator _mediator;
    private readonly IProductRepository _productRep;
    private readonly IProductGallleryRepository _galleryRep;
    private readonly IUserStoreRepository _userStoreRep;
    private readonly IProductPropertyRepository _productPropertyRep;
    private readonly IProductCateguryRepository _productCateguryRep;
    private readonly IProductStockPriceRepository _productStockRep;
    private readonly IProductQueries _productQueries;
    private readonly string _connectionString;

    public ProductDuplicateQueryHandler(IMediator mediator,
        IProductRepository productRep,
        IUserStoreRepository userStoreRep,
        IProductPropertyRepository productPropertyRep,
        IProductGallleryRepository galleryRep,
        IProductCateguryRepository productCateguryRep,
        IProductStockPriceRepository productStockRep,
        IProductQueries productQueries,
        IConfiguration configuration)
    {
        _mediator = mediator;
        _productRep = productRep;
        _userStoreRep = userStoreRep;
        _productCateguryRep = productCateguryRep;
        _productPropertyRep = productPropertyRep;
        _galleryRep = galleryRep;
        _productStockRep = productStockRep;
        _productQueries = productQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<Unit> Handle(ProductDuplicateQueryReq request, CancellationToken cancellationToken)
    {
        //var product = await _productRep.GetAsync(request.productId);

        ProductDto product = DapperHelper.ExecuteCommand<ProductDto>(_connectionString,
            conn => conn.Query<ProductDto>(_productQueries.GetOneProductFull, new { @Id = request.productId })
                .FirstOrDefault());

        //product.StoreId = store.Id;
        //product.RepId = store.RepId;
        //product.ShortLink = await _mediator.Send(new ProductShortLinkGeneratorQueryReq());
        //product.CreateDate = DateTime.Now;
        //product.Code = null;
        //product.Quantity = 0;
        //product.BasePrice = 0;
        //product.DiscountPrice = 0;
        //product.SalePrice = 0;
        //product.Price = 0;
        //product.Quantity = 0;
        product.Id = 0;
        product.Name = $"{product.Name}کپی محصول";
        product.Code = null;
        int productStockId = await _productStockRep.GetFirstProductStockPriceIdFromProductId(request.productId);
        //var productStock = await _productStockRep.GetAsync(productStockId);
        ProductStockPriceDto productStock = DapperHelper.ExecuteCommand<ProductStockPriceDto>(_connectionString,
            conn => conn
                .Query<ProductStockPriceDto>(_productQueries.GetProductStockPriceById, new { id = productStockId })
                .FirstOrDefault());
        product.StoreId = productStock.StoreId;


        var newProduct = await _mediator.Send(new ProductCreateCommandReq(product));


        List<Shared.Models.ProductProperty> properties =
            await _productPropertyRep.GetPropertiesByProduct(request.productId);
        foreach (Shared.Models.ProductProperty? property in properties)
        {
            property.Id = 0;
            property.ProductId = newProduct.Id;
            await _productPropertyRep.AddAsync(property);
        }

        List<Shared.Models.ProductCategury> categuries =
            await _productCateguryRep.GetCateguriesByProduct(request.productId);
        foreach (Shared.Models.ProductCategury? categury in categuries)
        {
            categury.Id = 0;
            categury.ProductId = newProduct.Id;
            await _productCateguryRep.AddAsync(categury);
        }

        List<Shared.Models.ProductGallery> galleries = await _galleryRep.GetGalleriesByProduct(request.productId);
        foreach (Shared.Models.ProductGallery? gallery in galleries)
        {
            gallery.Id = 0;
            gallery.ProductId = newProduct.Id;
            await _galleryRep.AddAsync(gallery);
        }

        return Unit.Value;
    }
}