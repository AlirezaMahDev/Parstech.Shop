using Parstech.Shop.Shared.Protos.Product;

namespace Parstech.Shop.Web.Services;

public class ProductGrpcClient : GrpcClientBase
{
    private readonly ProductService.ProductServiceClient _productService;
    private readonly ProductDetailService.ProductDetailServiceClient _productDetailService;
    private readonly ProductInventoryService.ProductInventoryServiceClient _inventoryService;
    private readonly ProductPriceService.ProductPriceServiceClient _priceService;

    public ProductGrpcClient(IConfiguration configuration) : base(configuration)
    {
        _productService = new ProductService.ProductServiceClient(Channel);
        _productDetailService = new ProductDetailService.ProductDetailServiceClient(Channel);
        _inventoryService = new ProductInventoryService.ProductInventoryServiceClient(Channel);
        _priceService = new ProductPriceService.ProductPriceServiceClient(Channel);
    }

    public async Task<ProductDto> GetProductAsync(long productId)
    {
        var request = new ProductIdsRequest { ProductIds = { productId } };
        var response = await _productService.GetProductsByIdsAsync(request);
        return response.Products.FirstOrDefault();
    }

    public async Task<ProductDto> GetProductByIdAsync(long productId)
    {
        return await GetProductAsync(productId);
    }

    public async Task<ProductsResponse> GetProductsAsync(int page = 1, int pageSize = 20, string sortBy = "CreatedDate", bool sortAscending = false)
    {
        var request = new ProductsRequest 
        { 
            Page = page,
            PageSize = pageSize,
            SortBy = sortBy,
            SortAscending = sortAscending
        };

        return await _productService.GetProductsAsync(request);
    }

    public async Task<ProductsResponse> SearchProductsAsync(string filter, int page = 1, int pageSize = 10)
    {
        var request = new SearchRequest 
        { 
            Query = filter,
            Page = page,
            PageSize = pageSize
        };

        return await _productService.SearchProductsAsync(request);
    }

    public async Task<ProductDetailResponse> GetProductDetailsAsync(long productId)
    {
        var request = new ProductDetailRequest { ProductId = productId };
        return await _productDetailService.GetProductDetailAsync(request);
    }

    public async Task<ProductGalleryResponse> GetProductGalleryAsync(long productId)
    {
        var request = new ProductDetailRequest { ProductId = productId };
        return await _productDetailService.GetProductGalleryAsync(request);
    }

    public async Task<ProductsResponse> GetProductsByCategoryAsync(long categoryId, int page = 1, int pageSize = 20)
    {
        var request = new CategoryProductsRequest 
        { 
            CategoryId = categoryId,
            Page = page,
            PageSize = pageSize,
            IncludeSubcategories = true
        };

        return await _productService.GetProductsByCategoryAsync(request);
    }

    public async Task<ProductsResponse> GetDiscountedProductsAsync(int take = 10)
    {
        var request = new DiscountedProductsRequest { Count = take };
        return await _productService.GetDiscountedProductsAsync(request);
    }

    public async Task<ProductAvailabilityResponse> CheckProductAvailabilityAsync(long productId, int quantity = 1)
    {
        var request = new ProductAvailabilityRequest
        {
            ProductId = productId,
            Quantity = quantity
        };
        
        return await _inventoryService.CheckProductAvailabilityAsync(request);
    }
}