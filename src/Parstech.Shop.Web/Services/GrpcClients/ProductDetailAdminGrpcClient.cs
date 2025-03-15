using Parstech.Shop.Shared.Protos.ProductDetailAdmin;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.ProductType;
using Shop.Application.DTOs.Property;
using Shop.Application.DTOs.PropertyCategury;
using Shop.Application.DTOs.RepresentationType;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.Tax;
using Shop.Application.DTOs.UserStore;
using System.Globalization;

namespace Parstech.Shop.Web.Services.GrpcClients
{
    public class ProductDetailAdminGrpcClient : GrpcClientBase
    {
        private readonly ProductDetailAdminService.ProductDetailAdminServiceClient _client;
        
        public ProductDetailAdminGrpcClient(IConfiguration configuration) : base(configuration)
        {
            _client = new ProductDetailAdminService.ProductDetailAdminServiceClient(Channel);
        }
        
        #region Product Operations
        
        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            var response = await _client.GetProductByIdAsync(request);
            
            return MapToProductDto(response);
        }
        
        public async Task<ResponseDto> CreateProductAsync(ProductDto product)
        {
            var request = MapFromProductDto(product);
            var response = await _client.CreateProductAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> UpdateProductAsync(ProductDto product)
        {
            var request = MapFromProductDto(product);
            var response = await _client.UpdateProductAsync(request);
            
            return MapToResponseDto(response);
        }
        
        #endregion
        
        #region Product Gallery Operations
        
        public async Task<List<ProductGalleryDto>> GetProductGalleryAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            var response = await _client.GetProductGalleryAsync(request);
            
            var result = new List<ProductGalleryDto>();
            foreach (var gallery in response.Galleries)
            {
                result.Add(MapToProductGalleryDto(gallery));
            }
            
            return result;
        }
        
        public async Task<ResponseDto> AddProductGalleryAsync(ProductGalleryDto gallery)
        {
            var request = MapFromProductGalleryDto(gallery);
            var response = await _client.AddProductGalleryAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> UpdateProductGalleryAsync(ProductGalleryDto gallery)
        {
            var request = MapFromProductGalleryDto(gallery);
            var response = await _client.UpdateProductGalleryAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> DeleteProductGalleryAsync(int galleryId)
        {
            var request = new ProductGalleryRequest { GalleryId = galleryId };
            var response = await _client.DeleteProductGalleryAsync(request);
            
            return MapToResponseDto(response);
        }
        
        #endregion
        
        #region Product Property Operations
        
        public async Task<List<ProductPropertyDto>> GetProductPropertiesAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            var response = await _client.GetProductPropertiesAsync(request);
            
            var result = new List<ProductPropertyDto>();
            foreach (var property in response.Properties)
            {
                result.Add(MapToProductPropertyDto(property));
            }
            
            return result;
        }
        
        public async Task<ResponseDto> AddProductPropertyAsync(ProductPropertyDto property)
        {
            var request = MapFromProductPropertyDto(property);
            var response = await _client.AddProductPropertyAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> UpdateProductPropertyAsync(ProductPropertyDto property)
        {
            var request = MapFromProductPropertyDto(property);
            var response = await _client.UpdateProductPropertyAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> DeleteProductPropertyAsync(int propertyId)
        {
            var request = new ProductPropertyRequest { ProductPropertyId = propertyId };
            var response = await _client.DeleteProductPropertyAsync(request);
            
            return MapToResponseDto(response);
        }
        
        #endregion
        
        #region Product Category Operations
        
        public async Task<List<ProductCateguryDto>> GetProductCategoriesAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            var response = await _client.GetProductCategoriesAsync(request);
            
            var result = new List<ProductCateguryDto>();
            foreach (var category in response.Categories)
            {
                result.Add(MapToProductCategoryDto(category));
            }
            
            return result;
        }
        
        public async Task<ResponseDto> AddProductCategoryAsync(ProductCateguryDto category)
        {
            var request = MapFromProductCategoryDto(category);
            var response = await _client.AddProductCategoryAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> DeleteProductCategoryAsync(int categoryId)
        {
            var request = new ProductCategoryRequest { ProductCategoryId = categoryId };
            var response = await _client.DeleteProductCategoryAsync(request);
            
            return MapToResponseDto(response);
        }
        
        #endregion
        
        #region Product Stock Price Operations
        
        public async Task<List<ProductStockPriceDto>> GetProductStockPricesAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            var response = await _client.GetProductStockPricesAsync(request);
            
            var result = new List<ProductStockPriceDto>();
            foreach (var stockPrice in response.StockPrices)
            {
                result.Add(MapToProductStockPriceDto(stockPrice));
            }
            
            return result;
        }
        
        public async Task<ResponseDto> AddProductStockPriceAsync(ProductStockPriceDto stockPrice)
        {
            var request = MapFromProductStockPriceDto(stockPrice);
            var response = await _client.AddProductStockPriceAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> UpdateProductStockPriceAsync(ProductStockPriceDto stockPrice)
        {
            var request = MapFromProductStockPriceDto(stockPrice);
            var response = await _client.UpdateProductStockPriceAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> DeleteProductStockPriceAsync(int stockPriceId)
        {
            var request = new ProductStockPriceRequest { ProductStockPriceId = stockPriceId };
            var response = await _client.DeleteProductStockPriceAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> UpdateStockQuantityPerBundleAsync(int stockPriceId, int quantityPerBundle)
        {
            var request = new UpdateQuantityRequest
            {
                ProductStockPriceId = stockPriceId,
                QuantityPerBundle = quantityPerBundle
            };
            
            var response = await _client.UpdateStockQuantityPerBundleAsync(request);
            
            return MapToResponseDto(response);
        }
        
        #endregion
        
        #region Product Representation Operations
        
        public async Task<List<ProductRepresentationDto>> GetProductRepresentationsAsync(int productId)
        {
            var request = new ProductRequest { ProductId = productId };
            var response = await _client.GetProductRepresentationsAsync(request);
            
            var result = new List<ProductRepresentationDto>();
            foreach (var representation in response.Representations)
            {
                result.Add(MapToProductRepresentationDto(representation));
            }
            
            return result;
        }
        
        public async Task<ResponseDto> AddProductRepresentationAsync(ProductRepresentationDto representation)
        {
            var request = MapFromProductRepresentationDto(representation);
            var response = await _client.AddProductRepresentationAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> QuickAddProductRepresentationAsync(ProductRepresentationDto representation)
        {
            var request = MapFromProductRepresentationDto(representation);
            var response = await _client.QuickAddProductRepresentationAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ResponseDto> DeleteProductRepresentationAsync(int representationId)
        {
            var request = new ProductRepresentationRequest { ProductRepresentationId = representationId };
            var response = await _client.DeleteProductRepresentationAsync(request);
            
            return MapToResponseDto(response);
        }
        
        #endregion
        
        #region Product Variation Operations
        
        public async Task<ResponseDto> AddProductVariationAsync(ProductDto variation)
        {
            var request = MapFromProductDto(variation);
            var response = await _client.AddProductVariationAsync(request);
            
            return MapToResponseDto(response);
        }
        
        public async Task<ProductDto> GetProductVariationAsync(int variationId)
        {
            var request = new ProductRequest { ProductId = variationId };
            var response = await _client.GetProductVariationAsync(request);
            
            return MapToProductDto(response);
        }
        
        public async Task<ResponseDto> DeleteProductVariationAsync(int variationId)
        {
            var request = new ProductRequest { ProductId = variationId };
            var response = await _client.DeleteProductVariationAsync(request);
            
            return MapToResponseDto(response);
        }
        
        #endregion
        
        #region Support Data Operations
        
        public async Task<List<CateguryDto>> GetCategoriesAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetCategoriesAsync(request);
            
            var result = new List<CateguryDto>();
            foreach (var category in response.Categories)
            {
                result.Add(MapToCategoryDto(category));
            }
            
            return result;
        }
        
        public async Task<List<CateguryDto>> GetSubCategoriesAsync(int parentId)
        {
            var request = new CategoryRequest { CategoryId = parentId };
            var response = await _client.GetSubCategoriesAsync(request);
            
            var result = new List<CateguryDto>();
            foreach (var category in response.Categories)
            {
                result.Add(MapToCategoryDto(category));
            }
            
            return result;
        }
        
        public async Task<List<PropertyCateguryDto>> GetPropertyCategoriesAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetPropertyCategoriesAsync(request);
            
            var result = new List<PropertyCateguryDto>();
            foreach (var category in response.Categories)
            {
                result.Add(MapToPropertyCategoryDto(category));
            }
            
            return result;
        }
        
        public async Task<List<PropertyDto>> GetPropertiesAsync(string filter, int categoryId)
        {
            var request = new PropertySearchRequest
            {
                Filter = filter ?? string.Empty,
                CategoryId = categoryId
            };
            
            var response = await _client.GetPropertiesAsync(request);
            
            var result = new List<PropertyDto>();
            foreach (var property in response.Properties)
            {
                result.Add(MapToPropertyDto(property));
            }
            
            return result;
        }
        
        public async Task<List<ProductTypeDto>> GetProductTypesAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetProductTypesAsync(request);
            
            var result = new List<ProductTypeDto>();
            foreach (var type in response.Types)
            {
                result.Add(MapToProductTypeDto(type));
            }
            
            return result;
        }
        
        public async Task<List<TaxDto>> GetTaxesAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetTaxesAsync(request);
            
            var result = new List<TaxDto>();
            foreach (var tax in response.Taxes)
            {
                result.Add(MapToTaxDto(tax));
            }
            
            return result;
        }
        
        public async Task<List<BrandDto>> GetBrandsAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetBrandsAsync(request);
            
            var result = new List<BrandDto>();
            foreach (var brand in response.Brands)
            {
                result.Add(MapToBrandDto(brand));
            }
            
            return result;
        }
        
        public async Task<List<UserStoreDto>> GetUserStoresAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetUserStoresAsync(request);
            
            var result = new List<UserStoreDto>();
            foreach (var store in response.Stores)
            {
                result.Add(MapToUserStoreDto(store));
            }
            
            return result;
        }
        
        public async Task<List<RepresentationTypeDto>> GetRepresentationTypesAsync()
        {
            var request = new EmptyRequest();
            var response = await _client.GetRepresentationTypesAsync(request);
            
            var result = new List<RepresentationTypeDto>();
            foreach (var type in response.Types)
            {
                result.Add(MapToRepresentationTypeDto(type));
            }
            
            return result;
        }
        
        #endregion
        
        #region Mapping Methods
        
        private ProductDto MapToProductDto(Shared.Protos.ProductDetailAdmin.ProductDto source)
        {
            DateTime createTime = DateTime.Now;
            DateTime lastChangeTime = DateTime.Now;
            
            if (!string.IsNullOrEmpty(source.CreateTime))
            {
                DateTime.TryParse(source.CreateTime, out createTime);
            }
            
            if (!string.IsNullOrEmpty(source.LastChangeTime))
            {
                DateTime.TryParse(source.LastChangeTime, out lastChangeTime);
            }
            
            var result = new ProductDto
            {
                Id = source.Id,
                Title = source.Title,
                LatinTitle = source.LatinTitle,
                Description = source.Description,
                Content = source.Content,
                Summary = source.Summary,
                ProductTypeId = source.ProductTypeId,
                IsActive = source.IsActive,
                IsDelete = source.IsDelete,
                BrandId = source.BrandId,
                ParentId = source.ParentId,
                TaxId = source.TaxId,
                Barcode = source.Barcode,
                Slug = source.Slug,
                ProductCode = source.ProductCode,
                MetaDescription = source.MetaDescription,
                MetaTitle = source.MetaTitle,
                CreateTime = createTime,
                LastChangeTime = lastChangeTime,
                ChangeByUserName = source.ChangeByUserName,
                StoreId = source.StoreId,
                Stock = source.Stock,
                AutoStock = source.AutoStock
            };
            
            return result;
        }
        
        private Shared.Protos.ProductDetailAdmin.ProductDto MapFromProductDto(ProductDto source)
        {
            var result = new Shared.Protos.ProductDetailAdmin.ProductDto
            {
                Id = source.Id,
                Title = source.Title ?? string.Empty,
                LatinTitle = source.LatinTitle ?? string.Empty,
                Description = source.Description ?? string.Empty,
                Content = source.Content ?? string.Empty,
                Summary = source.Summary ?? string.Empty,
                ProductTypeId = source.ProductTypeId,
                IsActive = source.IsActive,
                IsDelete = source.IsDelete,
                BrandId = source.BrandId,
                ParentId = source.ParentId,
                TaxId = source.TaxId,
                Barcode = source.Barcode ?? string.Empty,
                Slug = source.Slug ?? string.Empty,
                ProductCode = source.ProductCode ?? string.Empty,
                MetaDescription = source.MetaDescription ?? string.Empty,
                MetaTitle = source.MetaTitle ?? string.Empty,
                CreateTime = source.CreateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                LastChangeTime = source.LastChangeTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                ChangeByUserName = source.ChangeByUserName ?? string.Empty,
                StoreId = source.StoreId,
                Stock = source.Stock,
                AutoStock = source.AutoStock
            };
            
            return result;
        }
        
        private ProductGalleryDto MapToProductGalleryDto(Shared.Protos.ProductDetailAdmin.ProductGalleryDto source)
        {
            return new ProductGalleryDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                ImageName = source.ImageName,
                Sort = source.Sort,
                IsMain = source.IsMain,
                Alt = source.Alt
            };
        }
        
        private Shared.Protos.ProductDetailAdmin.ProductGalleryDto MapFromProductGalleryDto(ProductGalleryDto source)
        {
            return new Shared.Protos.ProductDetailAdmin.ProductGalleryDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                ImageName = source.ImageName ?? string.Empty,
                Sort = source.Sort,
                IsMain = source.IsMain,
                Alt = source.Alt ?? string.Empty
            };
        }
        
        private ProductPropertyDto MapToProductPropertyDto(Shared.Protos.ProductDetailAdmin.ProductPropertyDto source)
        {
            return new ProductPropertyDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                PropertyId = source.PropertyId,
                Value = source.Value,
                IsFilter = source.IsFilter,
                IsShow = source.IsShow,
                PropertyName = source.PropertyName
            };
        }
        
        private Shared.Protos.ProductDetailAdmin.ProductPropertyDto MapFromProductPropertyDto(ProductPropertyDto source)
        {
            return new Shared.Protos.ProductDetailAdmin.ProductPropertyDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                PropertyId = source.PropertyId,
                Value = source.Value ?? string.Empty,
                IsFilter = source.IsFilter,
                IsShow = source.IsShow,
                PropertyName = source.PropertyName ?? string.Empty
            };
        }
        
        private ProductCateguryDto MapToProductCategoryDto(Shared.Protos.ProductDetailAdmin.ProductCategoryDto source)
        {
            return new ProductCateguryDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                CateguryId = source.CategoryId,
                CateguryName = source.CategoryName
            };
        }
        
        private Shared.Protos.ProductDetailAdmin.ProductCategoryDto MapFromProductCategoryDto(ProductCateguryDto source)
        {
            return new Shared.Protos.ProductDetailAdmin.ProductCategoryDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                CategoryId = source.CateguryId,
                CategoryName = source.CateguryName ?? string.Empty
            };
        }
        
        private ProductStockPriceDto MapToProductStockPriceDto(Shared.Protos.ProductDetailAdmin.ProductStockPriceDto source)
        {
            DateTime? specialFromDate = null;
            DateTime? specialToDate = null;
            
            if (!string.IsNullOrEmpty(source.SpecialFromDate))
            {
                DateTime parsed;
                if (DateTime.TryParse(source.SpecialFromDate, out parsed))
                {
                    specialFromDate = parsed;
                }
            }
            
            if (!string.IsNullOrEmpty(source.SpecialToDate))
            {
                DateTime parsed;
                if (DateTime.TryParse(source.SpecialToDate, out parsed))
                {
                    specialToDate = parsed;
                }
            }
            
            return new ProductStockPriceDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                StoreId = source.StoreId,
                Price = source.Price,
                SpecialPrice = source.SpecialPrice,
                SpecialFromDate = specialFromDate,
                SpecialToDate = specialToDate,
                Stock = source.Stock,
                StockStatus = source.StockStatus,
                Cost = source.Cost,
                QuantityPerBundle = source.QuantityPerBundle,
                RepresentationId = source.RepresentationId,
                StoreName = source.StoreName,
                RepresentationName = source.RepresentationName
            };
        }
        
        private Shared.Protos.ProductDetailAdmin.ProductStockPriceDto MapFromProductStockPriceDto(ProductStockPriceDto source)
        {
            return new Shared.Protos.ProductDetailAdmin.ProductStockPriceDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                StoreId = source.StoreId,
                Price = source.Price,
                SpecialPrice = source.SpecialPrice,
                SpecialFromDate = source.SpecialFromDate?.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ?? string.Empty,
                SpecialToDate = source.SpecialToDate?.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ?? string.Empty,
                Stock = source.Stock,
                StockStatus = source.StockStatus,
                Cost = source.Cost,
                QuantityPerBundle = source.QuantityPerBundle,
                RepresentationId = source.RepresentationId,
                StoreName = source.StoreName ?? string.Empty,
                RepresentationName = source.RepresentationName ?? string.Empty
            };
        }
        
        private ProductRepresentationDto MapToProductRepresentationDto(Shared.Protos.ProductDetailAdmin.ProductRepresentationDto source)
        {
            return new ProductRepresentationDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                RepresentationTypeId = source.RepresentationTypeId,
                RepresentationTypeName = source.RepresentationTypeName,
                Value = source.Value
            };
        }
        
        private Shared.Protos.ProductDetailAdmin.ProductRepresentationDto MapFromProductRepresentationDto(ProductRepresentationDto source)
        {
            return new Shared.Protos.ProductDetailAdmin.ProductRepresentationDto
            {
                Id = source.Id,
                ProductId = source.ProductId,
                RepresentationTypeId = source.RepresentationTypeId,
                RepresentationTypeName = source.RepresentationTypeName ?? string.Empty,
                Value = source.Value ?? string.Empty
            };
        }
        
        private CateguryDto MapToCategoryDto(Shared.Protos.ProductDetailAdmin.CategoryDto source)
        {
            return new CateguryDto
            {
                GroupId = source.Id,
                GroupTitle = source.Title,
                LatinGroupTitle = source.LatinTitle,
                ParentId = source.ParentId,
                Image = source.Image,
                IsParnet = source.IsParent,
                Show = source.Show
            };
        }
        
        private PropertyCateguryDto MapToPropertyCategoryDto(Shared.Protos.ProductDetailAdmin.PropertyCategoryDto source)
        {
            return new PropertyCateguryDto
            {
                Id = source.Id,
                Name = source.Name
            };
        }
        
        private PropertyDto MapToPropertyDto(Shared.Protos.ProductDetailAdmin.PropertyDto source)
        {
            return new PropertyDto
            {
                Id = source.Id,
                Name = source.Name,
                PropertyCateguryId = source.PropertyCategoryId
            };
        }
        
        private ProductTypeDto MapToProductTypeDto(Shared.Protos.ProductDetailAdmin.ProductTypeDto source)
        {
            return new ProductTypeDto
            {
                Id = source.Id,
                Name = source.Name
            };
        }
        
        private TaxDto MapToTaxDto(Shared.Protos.ProductDetailAdmin.TaxDto source)
        {
            return new TaxDto
            {
                Id = source.Id,
                Name = source.Name,
                Percent = source.Percent
            };
        }
        
        private BrandDto MapToBrandDto(Shared.Protos.ProductDetailAdmin.BrandDto source)
        {
            return new BrandDto
            {
                BrandId = source.Id,
                BrandTitle = source.Title,
                LatinBrandTitle = source.LatinTitle,
                BrandImage = source.Image
            };
        }
        
        private UserStoreDto MapToUserStoreDto(Shared.Protos.ProductDetailAdmin.UserStoreDto source)
        {
            return new UserStoreDto
            {
                Id = source.Id,
                UserId = source.UserId,
                Name = source.Name,
                Mobile = source.Mobile
            };
        }
        
        private RepresentationTypeDto MapToRepresentationTypeDto(Shared.Protos.ProductDetailAdmin.RepresentationTypeDto source)
        {
            return new RepresentationTypeDto
            {
                Id = source.Id,
                Name = source.Name
            };
        }
        
        private ResponseDto MapToResponseDto(Shared.Protos.ProductDetailAdmin.ResponseDto source)
        {
            var result = new ResponseDto
            {
                IsSuccessed = source.IsSuccessed,
                Message = source.Message,
                Object = source.Object
            };
            
            if (source.Errors != null && source.Errors.Count > 0)
            {
                result.Errors = new List<FluentValidation.Results.ValidationFailure>();
                foreach (var error in source.Errors)
                {
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure(
                        error.PropertyName,
                        error.ErrorMessage
                    ));
                }
            }
            
            return result;
        }
        
        #endregion
    }
} 