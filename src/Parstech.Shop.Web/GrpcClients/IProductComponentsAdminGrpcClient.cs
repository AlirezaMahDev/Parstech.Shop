using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.GrpcClients;

public interface IProductComponentsAdminGrpcClient
{
    #region Gallery Operations

    Task<ProductGalleryListResponse> GetGalleriesOfProductAsync(int productId);
    Task<ResponseDto> CreateProductGalleryAsync(ProductGalleryDto gallery);
    Task<ResponseDto> DeleteProductGalleryAsync(int galleryId, int productId);
    Task<ResponseDto> ChangeMainGalleryAsync(int galleryId, int productId);

    #endregion

    #region Category Operations

    Task<ProductCategoryListResponse> GetCategoriesOfProductAsync(int productId);
    Task<ProductCategoryResponse> GetCategoryOfProductAsync(int productId);
    Task<CategoryListResponse> GetAllCategoriesAsync(string filter);
    Task<ProductIdResponse> DeleteProductCategoryAsync(int productId);

    #endregion

    #region Childs and Stock Operations

    Task<ChildsAndStocksResponse> GetChildsAndProductStocksAsync(int productId, int storeId);

    #endregion

    #region Feature Operations

    Task<ProductPropertyListResponse> GetPropertiesOfProductAsync(int productId);

    #endregion

    #region Product Detail Operations

    Task<ProductDetailResponse> GetProductDetailAsync(string shortLink, int storeId, string userName);

    #endregion
}