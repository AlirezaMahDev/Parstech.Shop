using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Services;

public interface IProductAdminGrpcClient
{
    // Product management operations
    Task<ProductPageingDto> GetProductsForAdminAsync(int currentPage,
        int takePage,
        string searchKey,
        string filterCat = "",
        string filter = "");

    Task<ProductDto> GetProductAsync(int productId);
    Task<ResponseDto> CreateProductAsync(ProductDto product);
    Task<ResponseDto> UpdateProductAsync(ProductDto product);
    Task<ResponseDto> UpdateProductQuickEditAsync(ProductQuickEditDto product);
    Task<ResponseDto> DuplicateProductAsync(int productId);
    Task<ResponseDto> DuplicateProductForStoreAsync(int productId, int storeId);
    Task<ResponseDto> DeleteProductAsync(int productId);

    // Gallery operations
    Task<GalleriesResponse> GetGalleriesOfProductAsync(int productId);
    Task<ProductGalleryDto> GetGalleryAsync(int galleryId);
    Task<ResponseDto> CreateGalleryAsync(ProductGalleryDto gallery);
    Task<ResponseDto> UpdateGalleryAsync(ProductGalleryDto gallery);

    // Property operations
    Task<PropertiesResponse> GetPropertiesOfProductAsync(int productId);
    Task<ProductPropertyDto> GetPropertyAsync(int propertyId);
    Task<ResponseDto> CreatePropertyAsync(ProductPropertyDto property);
    Task<ResponseDto> UpdatePropertyAsync(ProductPropertyDto property);

    // Category operations
    Task<CategoriesResponse> GetCategoriesOfProductAsync(int productId);
    Task<ProductCateguryDto> GetCategoryAsync(int categoryId);
    Task<ResponseDto> CreateCategoryAsync(ProductCateguryDto category);
    Task<ResponseDto> UpdateCategoryAsync(ProductCateguryDto category);
    Task<ResponseDto> DeleteCategoryAsync(ProductCateguryDto category);
    Task<CategoriesResponse> GetAllCategoriesAsync();

    // Other operations
    Task<ProductRepresentationDto> GetProductRepresentationAsync(int productId);
    Task<ProductParentsResponse> GetProductParentsAsync(string filter, int type);
    Task<ProductTypesResponse> GetProductTypesAsync();
    Task<TaxesResponse> GetTaxesAsync();
    Task<BrandsResponse> GetBrandsAsync();
    Task<UserStoresResponse> GetUserStoresAsync();
}