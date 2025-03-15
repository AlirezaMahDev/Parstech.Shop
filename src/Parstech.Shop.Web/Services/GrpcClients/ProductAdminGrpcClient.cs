using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Parstech.Shop.Shared.Protos.ProductAdmin;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class ProductAdminGrpcClient : GrpcClientBase
    {
        private readonly ProductAdminService.ProductAdminServiceClient _client;
        
        public ProductAdminGrpcClient(IConfiguration configuration) : base(configuration)
        {
            _client = new ProductAdminService.ProductAdminServiceClient(Channel);
        }
        
        // Product management operations
        public async Task<ProductPageingDto> GetProductsForAdminAsync(int currentPage, int takePage, string searchKey, string filterCat = "", string filter = "")
        {
            var request = new ProductParameterRequest
            {
                CurrentPage = currentPage,
                TakePage = takePage,
                SearchKey = searchKey ?? string.Empty,
                FilterCat = filterCat ?? string.Empty,
                Filter = filter ?? string.Empty
            };
            
            return await _client.GetProductsForAdminAsync(request);
        }
        
        public async Task<ProductDto> GetProductAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.GetProductAsync(request);
        }
        
        public async Task<ResponseDto> CreateProductAsync(ProductDto product)
        {
            return await _client.CreateProductAsync(product);
        }
        
        public async Task<ResponseDto> UpdateProductAsync(ProductDto product)
        {
            return await _client.UpdateProductAsync(product);
        }
        
        public async Task<ResponseDto> UpdateProductQuickEditAsync(ProductQuickEditDto product)
        {
            return await _client.UpdateProductQuickEditAsync(product);
        }
        
        public async Task<ResponseDto> DuplicateProductAsync(int productId)
        {
            var request = new ProductDuplicateRequest { ProductId = productId };
            return await _client.DuplicateProductAsync(request);
        }
        
        public async Task<ResponseDto> DuplicateProductForStoreAsync(int productId, int storeId)
        {
            var request = new ProductDuplicateForStoreRequest 
            { 
                ProductId = productId,
                StoreId = storeId
            };
            return await _client.DuplicateProductForStoreAsync(request);
        }
        
        public async Task<ResponseDto> DeleteProductAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.DeleteProductAsync(request);
        }
        
        // Gallery operations
        public async Task<GalleriesResponse> GetGalleriesOfProductAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.GetGalleriesOfProductAsync(request);
        }
        
        public async Task<ProductGalleryDto> GetGalleryAsync(int galleryId)
        {
            var request = new GalleryRequest { GalleryId = galleryId };
            return await _client.GetGalleryAsync(request);
        }
        
        public async Task<ResponseDto> CreateGalleryAsync(ProductGalleryDto gallery)
        {
            return await _client.CreateGalleryAsync(gallery);
        }
        
        public async Task<ResponseDto> UpdateGalleryAsync(ProductGalleryDto gallery)
        {
            return await _client.UpdateGalleryAsync(gallery);
        }
        
        // Property operations
        public async Task<PropertiesResponse> GetPropertiesOfProductAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.GetPropertiesOfProductAsync(request);
        }
        
        public async Task<ProductPropertyDto> GetPropertyAsync(int propertyId)
        {
            var request = new PropertyRequest { PropertyId = propertyId };
            return await _client.GetPropertyAsync(request);
        }
        
        public async Task<ResponseDto> CreatePropertyAsync(ProductPropertyDto property)
        {
            return await _client.CreatePropertyAsync(property);
        }
        
        public async Task<ResponseDto> UpdatePropertyAsync(ProductPropertyDto property)
        {
            return await _client.UpdatePropertyAsync(property);
        }
        
        // Category operations
        public async Task<CategoriesResponse> GetCategoriesOfProductAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.GetCategoriesOfProductAsync(request);
        }
        
        public async Task<ProductCateguryDto> GetCategoryAsync(int categoryId)
        {
            var request = new CategoryRequest { CategoryId = categoryId };
            return await _client.GetCategoryAsync(request);
        }
        
        public async Task<ResponseDto> CreateCategoryAsync(ProductCateguryDto category)
        {
            return await _client.CreateCategoryAsync(category);
        }
        
        public async Task<ResponseDto> UpdateCategoryAsync(ProductCateguryDto category)
        {
            return await _client.UpdateCategoryAsync(category);
        }
        
        public async Task<ResponseDto> DeleteCategoryAsync(ProductCateguryDto category)
        {
            return await _client.DeleteCategoryAsync(category);
        }
        
        public async Task<CategoriesResponse> GetAllCategoriesAsync()
        {
            return await _client.GetAllCategoriesAsync(new EmptyRequest());
        }
        
        // Representation operations
        public async Task<ProductRepresentationDto> GetProductRepresentationAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            return await _client.GetProductRepresentationAsync(request);
        }
        
        // Product Parents
        public async Task<ProductParentsResponse> GetProductParentsAsync(string filter, int type)
        {
            var request = new ProductParentsRequest 
            { 
                Filter = filter ?? string.Empty,
                Type = type 
            };
            return await _client.GetProductParentsAsync(request);
        }
        
        // Supplementary data
        public async Task<ProductTypesResponse> GetProductTypesAsync()
        {
            return await _client.GetProductTypesAsync(new EmptyRequest());
        }
        
        public async Task<TaxesResponse> GetTaxesAsync()
        {
            return await _client.GetTaxesAsync(new EmptyRequest());
        }
        
        public async Task<BrandsResponse> GetBrandsAsync()
        {
            return await _client.GetBrandsAsync(new EmptyRequest());
        }
        
        public async Task<UserStoresResponse> GetUserStoresAsync()
        {
            return await _client.GetUserStoresAsync(new EmptyRequest());
        }
    }
} 