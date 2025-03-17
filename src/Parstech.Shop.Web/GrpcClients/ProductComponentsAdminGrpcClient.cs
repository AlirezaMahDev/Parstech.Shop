using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.Web.GrpcClients;

public class ProductComponentsAdminGrpcClient : IProductComponentsAdminGrpcClient
{
    private readonly ProductComponentsAdminService.ProductComponentsAdminServiceClient _client;

    public ProductComponentsAdminGrpcClient(ProductComponentsAdminService.ProductComponentsAdminServiceClient client)
    {
        _client = client;
    }

    #region Gallery Operations

    public async Task<ProductGalleryListResponse> GetGalleriesOfProductAsync(int productId)
    {
        return await _client.GetGalleriesOfProductAsync(new ProductIdRequest { ProductId = productId });
    }

    public async Task<ResponseDto> CreateProductGalleryAsync(ProductGalleryDto gallery)
    {
        var request = new ProductGalleryRequest { Gallery = gallery };
        return await _client.CreateProductGalleryAsync(request);
    }

    public async Task<ResponseDto> DeleteProductGalleryAsync(int galleryId, int productId)
    {
        return await _client.DeleteProductGalleryAsync(new GalleryIdRequest
        {
            GalleryId = galleryId, ProductId = productId
        });
    }

    public async Task<ResponseDto> ChangeMainGalleryAsync(int galleryId, int productId)
    {
        return await _client.ChangeMainGalleryAsync(new ChangeMainGalleryRequest
        {
            GalleryId = galleryId, ProductId = productId
        });
    }

    #endregion

    #region Category Operations

    public async Task<ProductCategoryListResponse> GetCategoriesOfProductAsync(int productId)
    {
        return await _client.GetCategoriesOfProductAsync(new ProductIdRequest { ProductId = productId });
    }

    public async Task<ProductCategoryResponse> GetCategoryOfProductAsync(int productId)
    {
        return await _client.GetCategoryOfProductAsync(new ProductIdRequest { ProductId = productId });
    }

    public async Task<CategoryListResponse> GetAllCategoriesAsync(string filter)
    {
        return await _client.GetAllCategoriesAsync(new FilterRequest { Filter = filter ?? string.Empty });
    }

    public async Task<ProductIdResponse> DeleteProductCategoryAsync(int productId)
    {
        return await _client.DeleteProductCategoryAsync(new ProductIdRequest { ProductId = productId });
    }

    #endregion

    #region Childs and Stock Operations

    public async Task<ChildsAndStocksResponse> GetChildsAndProductStocksAsync(int productId, int storeId)
    {
        return await _client.GetChildsAndProductStocksAsync(new ChildsAndStocksRequest
        {
            ProductId = productId, StoreId = storeId
        });
    }

    #endregion

    #region Feature Operations

    public async Task<ProductPropertyListResponse> GetPropertiesOfProductAsync(int productId)
    {
        return await _client.GetPropertiesOfProductAsync(new ProductIdRequest { ProductId = productId });
    }

    #endregion

    #region Product Detail Operations

    public async Task<ProductDetailResponse> GetProductDetailAsync(string shortLink, int storeId, string userName)
    {
        return await _client.GetProductDetailAsync(new ProductDetailRequest
        {
            ShortLink = shortLink ?? string.Empty, StoreId = storeId, UserName = userName ?? string.Empty
        });
    }

    #endregion
}