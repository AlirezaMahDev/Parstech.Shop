using Grpc.Core;

using MediatR;

using System.Globalization;

using Parstech.Shop.ApiService.Application.Features.Brand.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductCategury.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductGallery.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductProperty.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.ProductType.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.PropertyCategury.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.Tax.Requests.Commands;
using Parstech.Shop.ApiService.Application.Features.UserStore.Requests.Commands;
using Parstech.Shop.ApiService.Application.Validators.ProductGallery;
using Parstech.Shop.Shared.DTOs;

using RepresentationAdminService = Parstech.Shop.Shared.Protos.RepresentationAdmin;

namespace Parstech.Shop.ApiService.Services;

public class ProductDetailAdminGrpcService : ProductDetailAdminService.ProductDetailAdminServiceBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ProductDetailAdminGrpcService> _logger;
    private readonly RepresentationAdminService.RepresentationAdminServiceBase _representationService;

    public ProductDetailAdminGrpcService(
        IMediator mediator,
        ILogger<ProductDetailAdminGrpcService> logger,
        RepresentationAdminService.RepresentationAdminServiceBase representationService)
    {
        _mediator = mediator;
        _logger = logger;
        _representationService = representationService;
    }

    #region Product Operations

    public override async Task<ProductDto> GetProductById(ProductRequest request, ServerCallContext context)
    {
        try
        {
            var product = await _mediator.Send(new ProductReadWithAllInfoCommandReq(request.ProductId));
            if (product == null)
            {
                throw new RpcException(new(StatusCode.NotFound, $"Product with ID {request.ProductId} not found"));
            }

            return MapToProductDto(product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product by ID {ProductId}", request.ProductId);
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving the product"));
        }
    }

    public override async Task<ResponseDto> CreateProduct(ProductDto request, ServerCallContext context)
    {
        try
        {
            var productDto = MapFromProductDto(request);

            // Validate product
            var validator = new ProductDtoValidator();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                ResponseDto response = new() { IsSuccessed = false, Object = request.ToString() };

                foreach (var error in validationResult.Errors)
                {
                    response.Errors.Add(new ValidationError
                    {
                        PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage
                    });
                }

                return response;
            }

            var result = await _mediator.Send(new ProductCreateCommandReq(productDto));

            return new() { IsSuccessed = true, Message = "محصول با موفقیت ثبت شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating product");
            return new() { IsSuccessed = false, Message = "خطا در ثبت محصول" };
        }
    }

    public override async Task<ResponseDto> UpdateProduct(ProductDto request, ServerCallContext context)
    {
        try
        {
            var productDto = MapFromProductDto(request);

            // Validate product
            var validator = new ProductDtoValidator();
            var validationResult = await validator.ValidateAsync(productDto);

            if (!validationResult.IsValid)
            {
                ResponseDto response = new() { IsSuccessed = false, Object = request.ToString() };

                foreach (var error in validationResult.Errors)
                {
                    response.Errors.Add(new ValidationError
                    {
                        PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage
                    });
                }

                return response;
            }

            var result = await _mediator.Send(new ProductUpdateCommandReq(productDto));

            return new() { IsSuccessed = true, Message = "محصول با موفقیت بروزرسانی شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product with ID {ProductId}", request.Id);
            return new() { IsSuccessed = false, Message = "خطا در بروزرسانی محصول" };
        }
    }

    #endregion

    #region Product Gallery Operations

    public override async Task<ProductGalleryListResponse> GetProductGallery(ProductRequest request,
        ServerCallContext context)
    {
        try
        {
            var galleries = await _mediator.Send(new ProductGalleryReadsByProductIdReq(request.ProductId));
            var response = new ProductGalleryListResponse();

            foreach (var gallery in galleries)
            {
                response.Galleries.Add(MapToProductGalleryDto(gallery));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product galleries for product ID {ProductId}", request.ProductId);
            throw new RpcException(new(StatusCode.Internal,
                "An error occurred while retrieving the product galleries"));
        }
    }

    public override async Task<ResponseDto> AddProductGallery(ProductGalleryDto request, ServerCallContext context)
    {
        try
        {
            var galleryDto = MapFromProductGalleryDto(request);

            // Validate gallery
            var validator = new ProductGalleryDtoValidator();
            var validationResult = await validator.ValidateAsync(galleryDto);

            if (!validationResult.IsValid)
            {
                ResponseDto response = new() { IsSuccessed = false, Object = request.ToString() };

                foreach (var error in validationResult.Errors)
                {
                    response.Errors.Add(new ValidationError
                    {
                        PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage
                    });
                }

                return response;
            }

            var result = await _mediator.Send(new ProductGalleryCreateCommandReq(galleryDto));

            return new() { IsSuccessed = true, Message = "تصویر محصول با موفقیت ثبت شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding product gallery");
            return new() { IsSuccessed = false, Message = "خطا در ثبت تصویر محصول" };
        }
    }

    public override async Task<ResponseDto> UpdateProductGallery(ProductGalleryDto request, ServerCallContext context)
    {
        try
        {
            var galleryDto = MapFromProductGalleryDto(request);

            // Validate gallery
            var validator = new ProductGalleryDtoValidator();
            var validationResult = await validator.ValidateAsync(galleryDto);

            if (!validationResult.IsValid)
            {
                ResponseDto response = new() { IsSuccessed = false, Object = request.ToString() };

                foreach (var error in validationResult.Errors)
                {
                    response.Errors.Add(new ValidationError
                    {
                        PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage
                    });
                }

                return response;
            }

            var result = await _mediator.Send(new ProductGalleryUpdateCommandReq(galleryDto));

            return new()
            {
                IsSuccessed = true, Message = "تصویر محصول با موفقیت بروزرسانی شد", Object = result.ToString()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product gallery with ID {GalleryId}", request.Id);
            return new() { IsSuccessed = false, Message = "خطا در بروزرسانی تصویر محصول" };
        }
    }

    public override async Task<ResponseDto> DeleteProductGallery(ProductGalleryRequest request,
        ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new ProductGalleryDeleteCommandReq(request.GalleryId));

            return new() { IsSuccessed = true, Message = "تصویر محصول با موفقیت حذف شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product gallery with ID {GalleryId}", request.GalleryId);
            return new() { IsSuccessed = false, Message = "خطا در حذف تصویر محصول" };
        }
    }

    #endregion

    #region Product Property Operations

    public override async Task<ProductPropertiesResponse> GetProductProperties(ProductRequest request,
        ServerCallContext context)
    {
        try
        {
            var properties = await _mediator.Send(new ProductPropertyReadsByProductIdReq(request.ProductId));
            var response = new ProductPropertiesResponse();

            foreach (var property in properties)
            {
                response.Properties.Add(MapToProductPropertyDto(property));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product properties for product ID {ProductId}", request.ProductId);
            throw new RpcException(
                new(StatusCode.Internal, "An error occurred while retrieving the product properties"));
        }
    }

    public override async Task<ResponseDto> AddProductProperty(ProductPropertyDto request, ServerCallContext context)
    {
        try
        {
            var propertyDto = MapFromProductPropertyDto(request);
            var result = await _mediator.Send(new ProductPropertyCreateCommandReq(propertyDto));

            return new() { IsSuccessed = true, Message = "ویژگی محصول با موفقیت ثبت شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding product property");
            return new() { IsSuccessed = false, Message = "خطا در ثبت ویژگی محصول" };
        }
    }

    public override async Task<ResponseDto> UpdateProductProperty(ProductPropertyDto request, ServerCallContext context)
    {
        try
        {
            var propertyDto = MapFromProductPropertyDto(request);
            var result = await _mediator.Send(new ProductPropertyUpdateCommandReq(propertyDto));

            return new()
            {
                IsSuccessed = true, Message = "ویژگی محصول با موفقیت بروزرسانی شد", Object = result.ToString()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating product property with ID {PropertyId}", request.Id);
            return new() { IsSuccessed = false, Message = "خطا در بروزرسانی ویژگی محصول" };
        }
    }

    public override async Task<ResponseDto> DeleteProductProperty(ProductPropertyRequest request,
        ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new ProductPropertyDeleteCommandReq(request.ProductPropertyId));

            return new() { IsSuccessed = true, Message = "ویژگی محصول با موفقیت حذف شد", Object = result.ToString() };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product property with ID {PropertyId}", request.ProductPropertyId);
            return new() { IsSuccessed = false, Message = "خطا در حذف ویژگی محصول" };
        }
    }

    #endregion

    #region Product Category Operations

    public override async Task<ProductCategoriesResponse> GetProductCategories(ProductRequest request,
        ServerCallContext context)
    {
        try
        {
            var categories = await _mediator.Send(new ProductCategoriesReadsByProductIdReq(request.ProductId));
            var response = new ProductCategoriesResponse();

            foreach (var category in categories)
            {
                response.Categories.Add(MapToProductCategoryDto(category));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product categories for product ID {ProductId}", request.ProductId);
            throw new RpcException(
                new(StatusCode.Internal, "An error occurred while retrieving the product categories"));
        }
    }

    public override async Task<ResponseDto> AddProductCategory(ProductCategoryDto request, ServerCallContext context)
    {
        try
        {
            var categoryDto = MapFromProductCategoryDto(request);
            var result = await _mediator.Send(new ProductCateguryCreateCommandReq(categoryDto));

            return new()
            {
                IsSuccessed = true, Message = "دسته بندی محصول با موفقیت ثبت شد", Object = result.ToString()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding product category");
            return new() { IsSuccessed = false, Message = "خطا در ثبت دسته بندی محصول" };
        }
    }

    public override async Task<ResponseDto> DeleteProductCategory(ProductCategoryRequest request,
        ServerCallContext context)
    {
        try
        {
            var result = await _mediator.Send(new ProductCateguryDeleteCommandReq(request.ProductCategoryId));

            return new()
            {
                IsSuccessed = true, Message = "دسته بندی محصول با موفقیت حذف شد", Object = result.ToString()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting product category with ID {CategoryId}", request.ProductCategoryId);
            return new() { IsSuccessed = false, Message = "خطا در حذف دسته بندی محصول" };
        }
    }

    #endregion

    #region Support Data Operations

    public override async Task<CategoriesResponse> GetCategories(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var categories = await _mediator.Send(new CateguryReadsCommandReq());
            var response = new CategoriesResponse();

            foreach (var category in categories)
            {
                response.Categories.Add(MapToCategoryDto(category));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting categories");
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving categories"));
        }
    }

    public override async Task<CategoriesResponse> GetSubCategories(CategoryRequest request, ServerCallContext context)
    {
        try
        {
            var categories = await _mediator.Send(new CateguryReadsByParentIdCommandReq(request.CategoryId));
            var response = new CategoriesResponse();

            foreach (var category in categories)
            {
                response.Categories.Add(MapToCategoryDto(category));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting subcategories for parent ID {ParentId}", request.CategoryId);
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving subcategories"));
        }
    }

    public override async Task<PropertyCategoriesResponse> GetPropertyCategories(EmptyRequest request,
        ServerCallContext context)
    {
        try
        {
            var categories = await _mediator.Send(new PropertyCateguryReadsCommandReq());
            var response = new PropertyCategoriesResponse();

            foreach (var category in categories)
            {
                response.Categories.Add(MapToPropertyCategoryDto(category));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting property categories");
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving property categories"));
        }
    }

    public override async Task<PropertiesResponse> GetProperties(PropertySearchRequest request,
        ServerCallContext context)
    {
        try
        {
            var properties = await _mediator.Send(new PropertiesReadCommandReq(request.Filter, request.CategoryId));
            var response = new PropertiesResponse();

            foreach (var property in properties)
            {
                response.Properties.Add(MapToPropertyDto(property));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error getting properties with filter {Filter} and category ID {CategoryId}",
                request.Filter,
                request.CategoryId);
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving properties"));
        }
    }

    public override async Task<ProductTypesResponse> GetProductTypes(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var types = await _mediator.Send(new ProductTypeReadsCommandReq());
            var response = new ProductTypesResponse();

            foreach (var type in types)
            {
                response.Types.Add(MapToProductTypeDto(type));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting product types");
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving product types"));
        }
    }

    public override async Task<TaxesResponse> GetTaxes(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var taxes = await _mediator.Send(new TaxReadsCommandReq());
            var response = new TaxesResponse();

            foreach (var tax in taxes)
            {
                response.Taxes.Add(MapToTaxDto(tax));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting taxes");
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving taxes"));
        }
    }

    public override async Task<BrandsResponse> GetBrands(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var brands = await _mediator.Send(new BrandReadsCommandReq());
            var response = new BrandsResponse();

            foreach (var brand in brands)
            {
                response.Brands.Add(MapToBrandDto(brand));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting brands");
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving brands"));
        }
    }

    public override async Task<UserStoresResponse> GetUserStores(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var stores = await _mediator.Send(new UserStoreReadCommandReq());
            var response = new UserStoresResponse();

            foreach (var store in stores)
            {
                response.Stores.Add(MapToUserStoreDto(store));
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user stores");
            throw new RpcException(new(StatusCode.Internal, "An error occurred while retrieving user stores"));
        }
    }

    public override async Task<RepresentationTypesResponse> GetRepresentationTypes(EmptyRequest request,
        ServerCallContext context)
    {
        try
        {
            // Use the dedicated representation service to get representation types
            var representationClient =
                new RepresentationAdminService.RepresentationAdminServiceClient(context.GetHttpContext()
                    .GetGrpcChannel());
            var typesResponse =
                await representationClient.GetRepresentationTypesAsync(new RepresentationAdminService.EmptyRequest());

            var response = new RepresentationTypesResponse();
            foreach (var type in typesResponse.Types)
            {
                response.Types.Add(new RepresentationTypeDto { Id = type.Id, Name = type.Name });
            }

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetRepresentationTypes: {Message}", ex.Message);
            throw new RpcException(new(StatusCode.Internal, $"An error occurred: {ex.Message}"));
        }
    }

    #endregion

    #region Mapping Methods

    private ProductDto MapToProductDto(Shop.Application.DTOs.Product.ProductDto source)
    {
        ProductDto result = new()
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

        // Map related entities if available
        if (source.ProductGalleries != null)
        {
            foreach (var gallery in source.ProductGalleries)
            {
                result.Galleries.Add(MapToProductGalleryDto(gallery));
            }
        }

        if (source.ProductProperties != null)
        {
            foreach (var property in source.ProductProperties)
            {
                result.Properties.Add(MapToProductPropertyDto(property));
            }
        }

        if (source.ProductCateguries != null)
        {
            foreach (var category in source.ProductCateguries)
            {
                result.Categories.Add(MapToProductCategoryDto(category));
            }
        }

        if (source.ProductStockPrices != null)
        {
            foreach (var stockPrice in source.ProductStockPrices)
            {
                result.StockPrices.Add(MapToProductStockPriceDto(stockPrice));
            }
        }

        if (source.ProductRepresentations != null)
        {
            foreach (var representation in source.ProductRepresentations)
            {
                result.Representations.Add(MapToProductRepresentationDto(representation));
            }
        }

        if (source.Variations != null)
        {
            foreach (var variation in source.Variations)
            {
                result.Variations.Add(MapToProductDto(variation));
            }
        }

        return result;
    }

    private Parstech.Shop.Application.DTOs.Product.ProductDto MapFromProductDto(ProductDto source)
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

        return new Parstech.Shop.Application.DTOs.Product.ProductDto
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
    }

    private ProductGalleryDto MapToProductGalleryDto(Shop.Application.DTOs.ProductGallery.ProductGalleryDto source)
    {
        return new()
        {
            Id = source.Id,
            ProductId = source.ProductId,
            ImageName = source.ImageName ?? string.Empty,
            Sort = source.Sort,
            IsMain = source.IsMain,
            Alt = source.Alt ?? string.Empty
        };
    }

    private Parstech.Shop.Application.DTOs.ProductGallery.ProductGalleryDto MapFromProductGalleryDto(ProductGalleryDto source)
    {
        return new Parstech.Shop.Application.DTOs.ProductGallery.ProductGalleryDto
        {
            Id = source.Id,
            ProductId = source.ProductId,
            ImageName = source.ImageName,
            Sort = source.Sort,
            IsMain = source.IsMain,
            Alt = source.Alt
        };
    }

    private ProductPropertyDto MapToProductPropertyDto(Shop.Application.DTOs.ProductProperty.ProductPropertyDto source)
    {
        return new()
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

    private Parstech.Shop.Application.DTOs.ProductProperty.ProductPropertyDto MapFromProductPropertyDto(
        ProductPropertyDto source)
    {
        return new Parstech.Shop.Application.DTOs.ProductProperty.ProductPropertyDto
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

    private ProductCategoryDto MapToProductCategoryDto(Shop.Application.DTOs.ProductCategury.ProductCateguryDto source)
    {
        return new ProductCategoryDto
        {
            Id = source.Id,
            ProductId = source.ProductId,
            CategoryId = source.CateguryId,
            CategoryName = source.CateguryName ?? string.Empty
        };
    }

    private Parstech.Shop.Application.DTOs.ProductCategury.ProductCateguryDto MapFromProductCategoryDto(
        ProductCategoryDto source)
    {
        return new Parstech.Shop.Application.DTOs.ProductCategury.ProductCateguryDto
        {
            Id = source.Id,
            ProductId = source.ProductId,
            CateguryId = source.CategoryId,
            CateguryName = source.CategoryName
        };
    }

    private ProductStockPriceDto MapToProductStockPriceDto(
        Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto source)
    {
        return new()
        {
            Id = source.Id,
            ProductId = source.ProductId,
            StoreId = source.StoreId,
            Price = source.Price,
            SpecialPrice = source.SpecialPrice,
            SpecialFromDate =
                source.SpecialFromDate?.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ?? string.Empty,
            SpecialToDate =
                source.SpecialToDate?.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ?? string.Empty,
            Stock = source.Stock,
            StockStatus = source.StockStatus,
            Cost = source.Cost,
            QuantityPerBundle = source.QuantityPerBundle,
            RepresentationId = source.RepresentationId,
            StoreName = source.StoreName ?? string.Empty,
            RepresentationName = source.RepresentationName ?? string.Empty
        };
    }

    private Parstech.Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto MapFromProductStockPriceDto(
        ProductStockPriceDto source)
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

        return new Parstech.Shop.Application.DTOs.ProductStockPrice.ProductStockPriceDto
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

    private ProductRepresentationDto MapToProductRepresentationDto(
        Shop.Application.DTOs.ProductRepresentation.ProductRepresentationDto source)
    {
        return new()
        {
            Id = source.Id,
            ProductId = source.ProductId,
            RepresentationTypeId = source.RepresentationTypeId,
            RepresentationTypeName = source.RepresentationTypeName ?? string.Empty,
            Value = source.Value ?? string.Empty
        };
    }

    private Parstech.Shop.Application.DTOs.ProductRepresentation.ProductRepresentationDto MapFromProductRepresentationDto(
        ProductRepresentationDto source)
    {
        return new Parstech.Shop.Application.DTOs.ProductRepresentation.ProductRepresentationDto
        {
            Id = source.Id,
            ProductId = source.ProductId,
            RepresentationTypeId = source.RepresentationTypeId,
            RepresentationTypeName = source.RepresentationTypeName,
            Value = source.Value
        };
    }

    private CategoryDto MapToCategoryDto(Shop.Application.DTOs.Categury.CateguryDto source)
    {
        return new CategoryDto
        {
            Id = source.GroupId,
            Title = source.GroupTitle ?? string.Empty,
            LatinTitle = source.LatinGroupTitle ?? string.Empty,
            ParentId = source.ParentId,
            Image = source.Image ?? string.Empty,
            IsParent = source.IsParnet,
            Show = source.Show
        };
    }

    private PropertyCategoryDto MapToPropertyCategoryDto(
        Shop.Application.DTOs.PropertyCategury.PropertyCateguryDto source)
    {
        return new PropertyCategoryDto { Id = source.Id, Name = source.Name ?? string.Empty };
    }

    private PropertyDto MapToPropertyDto(Shop.Application.DTOs.Property.PropertyDto source)
    {
        return new()
        {
            Id = source.Id, Name = source.Name ?? string.Empty, PropertyCategoryId = source.PropertyCateguryId
        };
    }

    private ProductTypeDto MapToProductTypeDto(Shop.Application.DTOs.ProductType.ProductTypeDto source)
    {
        return new() { Id = source.Id, Name = source.Name ?? string.Empty };
    }

    private TaxDto MapToTaxDto(Shop.Application.DTOs.Tax.TaxDto source)
    {
        return new() { Id = source.Id, Name = source.Name ?? string.Empty, Percent = source.Percent };
    }

    private BrandDto MapToBrandDto(Shop.Application.DTOs.Brand.BrandDto source)
    {
        return new()
        {
            Id = source.BrandId,
            Title = source.BrandTitle ?? string.Empty,
            LatinTitle = source.LatinBrandTitle ?? string.Empty,
            Image = source.BrandImage ?? string.Empty
        };
    }

    private UserStoreDto MapToUserStoreDto(Shop.Application.DTOs.UserStore.UserStoreDto source)
    {
        return new()
        {
            Id = source.Id,
            UserId = source.UserId ?? string.Empty,
            Name = source.Name ?? string.Empty,
            Mobile = source.Mobile ?? string.Empty
        };
    }

    private RepresentationTypeDto MapToRepresentationTypeDto(
        Shop.Application.DTOs.RepresentationType.RepresentationTypeDto source)
    {
        return new() { Id = source.Id, Name = source.Name ?? string.Empty };
    }

    #endregion
}