using MediatR;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class ProductDeleteQueryHandler : IRequestHandler<ProductDeleteQueryReq, bool>
{
    private IOrderDetailRepository _orderDetailRep;
    private IProductRepository _productRep;
    private IProductStockPriceRepository _productStockPriceRep;
    private IProductCateguryRepository _productCateguryRep;
    private IProductPropertyRepository _productPropertyRep;
    private IProductGallleryRepository _productGalleryRep;

    public ProductDeleteQueryHandler(IOrderDetailRepository orderDetailRep,
        IProductRepository productRep,
        IProductStockPriceRepository productStockPriceRep,
        IProductCateguryRepository productCateguryRep,
        IProductPropertyRepository productPropertyRep,
        IProductGallleryRepository productGalleryRep)
    {
        _productRep = productRep;
        _orderDetailRep = orderDetailRep;
        _productStockPriceRep = productStockPriceRep;
        _productCateguryRep = productCateguryRep;
        _productPropertyRep = productPropertyRep;
        _productGalleryRep = productGalleryRep;
    }

    public async Task<bool> Handle(ProductDeleteQueryReq request, CancellationToken cancellationToken)
    {
        Shared.Models.Product? product = await _productRep.GetAsync(request.productId);
        List<Shared.Models.ProductStockPrice> productStockPriceList =
            await _productStockPriceRep.GetAllByProductId(request.productId);
        if (productStockPriceList.Count > 0)
        {
            return false;
        }

        List<Shared.Models.ProductGallery> galleries =
            await _productGalleryRep.GetGalleriesByProduct(request.productId);
        List<Shared.Models.ProductProperty> properties =
            await _productPropertyRep.GetPropertiesByProduct(request.productId);
        List<Shared.Models.ProductCategury> Categuries =
            await _productCateguryRep.GetCateguriesByProduct(request.productId);
        foreach (Shared.Models.ProductGallery? gallery in galleries)
        {
            await _productGalleryRep.DeleteAsync(gallery);
        }

        foreach (Shared.Models.ProductProperty? property in properties)
        {
            await _productPropertyRep.DeleteAsync(property);
        }

        foreach (Shared.Models.ProductCategury? categury in Categuries)
        {
            await _productCateguryRep.DeleteAsync(categury);
        }

        await _productRep.DeleteAsync(product);
        return true;
    }
}