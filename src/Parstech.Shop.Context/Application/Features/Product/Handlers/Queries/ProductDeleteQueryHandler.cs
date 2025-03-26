using MediatR;

using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

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
        var product =await _productRep.GetAsync(request.productId);
        var productStockPriceList =await _productStockPriceRep.GetAllByProductId(request.productId);
        if(productStockPriceList.Count >0)
        {
            return false;
        }
        var galleries=await _productGalleryRep.GetGalleriesByProduct(request.productId);
        var properties=await _productPropertyRep.GetPropertiesByProduct(request.productId);
        var Categuries=await _productCateguryRep.GetCateguriesByProduct(request.productId);
        foreach(var gallery in galleries)
        {
            await _productGalleryRep.DeleteAsync(gallery);
        }
        foreach(var property in properties)
        {
            await _productPropertyRep.DeleteAsync(property);
        }
        foreach(var categury in Categuries)
        {
            await _productCateguryRep.DeleteAsync(categury);
        }
        await _productRep.DeleteAsync(product);
        return true;

    }
}