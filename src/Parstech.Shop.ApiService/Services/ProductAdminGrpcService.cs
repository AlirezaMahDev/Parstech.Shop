using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Parstech.Shop.Shared.Protos.ProductAdmin;
using Shop.Application.Features.Brand.Requests.Commands;
using Shop.Application.Features.Categury.Requests.Commands;
using Shop.Application.Features.Product.Requests.Commands;
using Shop.Application.Features.Product.Requests.Queries;
using Shop.Application.Features.ProductCategury.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Commands;
using Shop.Application.Features.ProductGallery.Requests.Queries;
using Shop.Application.Features.ProductProperty.Requests.Commands;
using Shop.Application.Features.ProductProperty.Requests.Queries;
using Shop.Application.Features.ProductRepresentation.Requests.Queries;
using Shop.Application.Features.ProductType.Requests.Commands;
using Shop.Application.Features.Tax.Requests.Commands;
using Shop.Application.Features.UserStore.Requests.Commands;

namespace Shop.ApiService.Services
{
    public class ProductAdminGrpcService : ProductAdminService.ProductAdminServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        
        public ProductAdminGrpcService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        
        public override async Task<ProductPageingDto> GetProductsForAdmin(ProductParameterRequest request, ServerCallContext context)
        {
            var parameter = new Shop.Application.DTOs.Product.ProductParameterDto
            {
                CurrentPage = request.CurrentPage,
                TakePage = request.TakePage,
                SearchKey = request.SearchKey,
                FilterCat = request.FilterCat,
                Filter = request.Filter
            };
            
            var result = await _mediator.Send(new ProductPagingForAdminQueryReq(parameter));
            var response = new ProductPageingDto
            {
                CurrentPage = result.CurrentPage,
                PageCount = result.PageCount,
                RowCount = result.RowCount
            };
            
            foreach (var product in result.List)
            {
                response.List.Add(MapToProductDto(product));
            }
            
            return response;
        }
        
        public override async Task<ProductDto> GetProduct(ProductRequest request, ServerCallContext context)
        {
            var product = await _mediator.Send(new ProductReadCommandReq(request.ProductId));
            return MapToProductDto(product);
        }
        
        public override async Task<ResponseDto> CreateProduct(ProductDto request, ServerCallContext context)
        {
            var productDto = MapFromProductDto(request);
            await _mediator.Send(new ProductCreateCommandReq(productDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "محصول با موفقیت ثبت شد",
                Object = Any.Pack(request)
            };
        }
        
        public override async Task<ResponseDto> UpdateProduct(ProductDto request, ServerCallContext context)
        {
            var productDto = MapFromProductDto(request);
            await _mediator.Send(new ProductUpdateCommandReq(productDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "محصول با موفقیت ویرایش شد",
                Object = Any.Pack(request)
            };
        }
        
        public override async Task<ResponseDto> UpdateProductQuickEdit(ProductQuickEditDto request, ServerCallContext context)
        {
            var quickEditDto = new Shop.Application.DTOs.Product.ProductQuickEditDto
            {
                Id = request.Id,
                ProductId = request.ProductId,
                Code = request.Code,
                Name = request.Name,
                LatinName = request.LatinName,
                TypeId = request.TypeId,
                VariationName = request.VariationName,
                StoreId = request.StoreId,
                ParentId = request.ParentId,
                BrandId = request.BrandId,
                TaxId = request.TaxId,
                Score = request.Score,
                QuantityPerBundle = request.QuantityPerBundle
            };
            
            await _mediator.Send(new ProductQuickEditQueryReq(quickEditDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "محصول با موفقیت ویرایش شد",
                Object = Any.Pack(request)
            };
        }
        
        public override async Task<ResponseDto> DuplicateProduct(ProductDuplicateRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ProductDuplicateQueryReq(request.ProductId));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "محصول با موفقیت کپی شد"
            };
        }
        
        public override async Task<ResponseDto> DuplicateProductForStore(ProductDuplicateForStoreRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ProductDuplicateForStoreQueryReq(request.ProductId, request.StoreId));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "محصول با موفقیت برای فروشگاه مورد نظر کپی شد"
            };
        }
        
        public override async Task<ResponseDto> DeleteProduct(ProductRequest request, ServerCallContext context)
        {
            await _mediator.Send(new ProductDeleteCommandReq(request.ProductId));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "محصول با موفقیت حذف شد"
            };
        }
        
        public override async Task<GalleriesResponse> GetGalleriesOfProduct(ProductRequest request, ServerCallContext context)
        {
            var galleries = await _mediator.Send(new GalleriesOfProductQueryReq(request.ProductId));
            var response = new GalleriesResponse();
            
            foreach (var gallery in galleries)
            {
                response.Galleries.Add(new ProductGalleryDto
                {
                    Id = gallery.Id,
                    ProductId = gallery.ProductId,
                    Title = gallery.Title ?? string.Empty,
                    Image = gallery.Image ?? string.Empty,
                    Order = gallery.Order
                });
            }
            
            return response;
        }
        
        public override async Task<ProductGalleryDto> GetGallery(GalleryRequest request, ServerCallContext context)
        {
            var gallery = await _mediator.Send(new ProductGalleryReadCommandReq(request.GalleryId));
            
            return new ProductGalleryDto
            {
                Id = gallery.Id,
                ProductId = gallery.ProductId,
                Title = gallery.Title ?? string.Empty,
                Image = gallery.Image ?? string.Empty,
                Order = gallery.Order
            };
        }
        
        public override async Task<ResponseDto> CreateGallery(ProductGalleryDto request, ServerCallContext context)
        {
            var galleryDto = new Shop.Application.DTOs.ProductGallery.ProductGalleryDto
            {
                ProductId = request.ProductId,
                Title = request.Title,
                Image = request.Image,
                Order = request.Order
            };
            
            await _mediator.Send(new ProductGalleryCreateCommandReq(galleryDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "گالری با موفقیت ایجاد شد"
            };
        }
        
        public override async Task<ResponseDto> UpdateGallery(ProductGalleryDto request, ServerCallContext context)
        {
            var galleryDto = new Shop.Application.DTOs.ProductGallery.ProductGalleryDto
            {
                Id = request.Id,
                ProductId = request.ProductId,
                Title = request.Title,
                Image = request.Image,
                Order = request.Order
            };
            
            await _mediator.Send(new ProductGalleryUpdateCommandReq(galleryDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "گالری با موفقیت بروزرسانی شد"
            };
        }
        
        public override async Task<PropertiesResponse> GetPropertiesOfProduct(ProductRequest request, ServerCallContext context)
        {
            var properties = await _mediator.Send(new PropertiesOfProductQueryReq(request.ProductId));
            var response = new PropertiesResponse();
            
            foreach (var property in properties)
            {
                response.Properties.Add(new ProductPropertyDto
                {
                    Id = property.Id,
                    ProductId = property.ProductId,
                    PropertyId = property.PropertyId,
                    PropertyName = property.PropertyName ?? string.Empty,
                    Value = property.Value ?? string.Empty,
                    Order = property.Order
                });
            }
            
            return response;
        }
        
        public override async Task<ProductPropertyDto> GetProperty(PropertyRequest request, ServerCallContext context)
        {
            var property = await _mediator.Send(new ProductPropertyReadCommandReq(request.PropertyId));
            
            return new ProductPropertyDto
            {
                Id = property.Id,
                ProductId = property.ProductId,
                PropertyId = property.PropertyId,
                PropertyName = property.PropertyName ?? string.Empty,
                Value = property.Value ?? string.Empty,
                Order = property.Order
            };
        }
        
        public override async Task<ResponseDto> CreateProperty(ProductPropertyDto request, ServerCallContext context)
        {
            var propertyDto = new Shop.Application.DTOs.ProductProperty.ProductPropertyDto
            {
                ProductId = request.ProductId,
                PropertyId = request.PropertyId,
                PropertyName = request.PropertyName,
                Value = request.Value,
                Order = request.Order
            };
            
            await _mediator.Send(new ProductPropertyCreateCommandReq(propertyDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "مشخصه با موفقیت ایجاد شد"
            };
        }
        
        public override async Task<ResponseDto> UpdateProperty(ProductPropertyDto request, ServerCallContext context)
        {
            var propertyDto = new Shop.Application.DTOs.ProductProperty.ProductPropertyDto
            {
                Id = request.Id,
                ProductId = request.ProductId,
                PropertyId = request.PropertyId,
                PropertyName = request.PropertyName,
                Value = request.Value,
                Order = request.Order
            };
            
            await _mediator.Send(new ProductPropertyUpdateCommandReq(propertyDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "مشخصه با موفقیت بروزرسانی شد"
            };
        }
        
        public override async Task<CategoriesResponse> GetCategoriesOfProduct(ProductRequest request, ServerCallContext context)
        {
            var categories = await _mediator.Send(new ProductCategoriesReadCommandReq(request.ProductId));
            var response = new CategoriesResponse();
            
            foreach (var category in categories)
            {
                response.Categories.Add(new ProductCateguryDto
                {
                    Id = category.Id,
                    ProductId = category.ProductId,
                    CateguryId = category.CateguryId,
                    CateguryName = category.CateguryName ?? string.Empty
                });
            }
            
            return response;
        }
        
        public override async Task<ProductCateguryDto> GetCategory(CategoryRequest request, ServerCallContext context)
        {
            var category = await _mediator.Send(new ProductCateguryReadCommandReq(request.CategoryId));
            
            return new ProductCateguryDto
            {
                Id = category.Id,
                ProductId = category.ProductId,
                CateguryId = category.CateguryId,
                CateguryName = category.CateguryName ?? string.Empty
            };
        }
        
        public override async Task<ResponseDto> CreateCategory(ProductCateguryDto request, ServerCallContext context)
        {
            var categoryDto = new Shop.Application.DTOs.ProductCategury.ProductCateguryDto
            {
                ProductId = request.ProductId,
                CateguryId = request.CateguryId,
                CateguryName = request.CateguryName
            };
            
            await _mediator.Send(new ProductCateguryCreateCommandReq(categoryDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "دسته بندی با موفقیت اضافه شد"
            };
        }
        
        public override async Task<ResponseDto> UpdateCategory(ProductCateguryDto request, ServerCallContext context)
        {
            var categoryDto = new Shop.Application.DTOs.ProductCategury.ProductCateguryDto
            {
                Id = request.Id,
                ProductId = request.ProductId,
                CateguryId = request.CateguryId,
                CateguryName = request.CateguryName
            };
            
            await _mediator.Send(new ProductCateguryUpdateCommandReq(categoryDto));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "دسته بندی با موفقیت بروزرسانی شد"
            };
        }
        
        public override async Task<ResponseDto> DeleteCategory(ProductCateguryDto request, ServerCallContext context)
        {
            await _mediator.Send(new ProductCateguryDeleteCommandReq(request.Id));
            
            return new ResponseDto
            {
                IsSuccessed = true,
                Message = "دسته بندی با موفقیت حذف شد"
            };
        }
        
        public override async Task<CategoriesResponse> GetAllCategories(EmptyRequest request, ServerCallContext context)
        {
            var categories = await _mediator.Send(new CateguryReadsCommandReq());
            var response = new CategoriesResponse();
            
            foreach (var category in categories)
            {
                response.Categories.Add(new ProductCateguryDto
                {
                    CateguryId = category.Id,
                    CateguryName = category.Title ?? string.Empty
                });
            }
            
            return response;
        }
        
        public override async Task<ProductRepresentationDto> GetProductRepresentation(ProductRequest request, ServerCallContext context)
        {
            var rep = await _mediator.Send(new ProductRepresentationsReadQueryReq(request.ProductId));
            
            if (rep == null)
            {
                return new ProductRepresentationDto();
            }
            
            return new ProductRepresentationDto
            {
                Id = rep.Id,
                ProductId = rep.ProductId,
                RepresentationId = rep.RepresentationId,
                RepresentationName = rep.RepresentationName ?? string.Empty
            };
        }
        
        public override async Task<ProductParentsResponse> GetProductParents(ProductParentsRequest request, ServerCallContext context)
        {
            var parents = await _mediator.Send(new ProductParentsSearchQueryReq(request.Filter, request.Type));
            var response = new ProductParentsResponse();
            
            foreach (var parent in parents)
            {
                response.Parents.Add(MapToProductDto(parent));
            }
            
            return response;
        }
        
        public override async Task<ProductTypesResponse> GetProductTypes(EmptyRequest request, ServerCallContext context)
        {
            var types = await _mediator.Send(new ProductTypeReadsCommandReq());
            var response = new ProductTypesResponse();
            
            foreach (var type in types)
            {
                response.Types.Add(new ProductTypeDto 
                { 
                    Id = type.Id,
                    Name = type.Name ?? string.Empty
                });
            }
            
            return response;
        }
        
        public override async Task<TaxesResponse> GetTaxes(EmptyRequest request, ServerCallContext context)
        {
            var taxes = await _mediator.Send(new TaxReadsCommandReq());
            var response = new TaxesResponse();
            
            foreach (var tax in taxes)
            {
                response.Taxes.Add(new TaxDto 
                { 
                    Id = tax.Id,
                    Title = tax.Title ?? string.Empty,
                    Percent = tax.Percent,
                    Code = tax.Code ?? string.Empty
                });
            }
            
            return response;
        }
        
        public override async Task<BrandsResponse> GetBrands(EmptyRequest request, ServerCallContext context)
        {
            var brands = await _mediator.Send(new BrandReadsCommandReq());
            var response = new BrandsResponse();
            
            foreach (var brand in brands)
            {
                response.Brands.Add(new BrandDto 
                { 
                    Id = brand.Id,
                    Name = brand.Name ?? string.Empty,
                    LatinName = brand.LatinName ?? string.Empty,
                    IsActive = brand.IsActive,
                    Logo = brand.Logo ?? string.Empty
                });
            }
            
            return response;
        }
        
        public override async Task<UserStoresResponse> GetUserStores(EmptyRequest request, ServerCallContext context)
        {
            var stores = await _mediator.Send(new UserStoreReadsCommandReq());
            var response = new UserStoresResponse();
            
            foreach (var store in stores)
            {
                response.Stores.Add(new UserStoreDto 
                { 
                    Id = store.Id,
                    UserId = store.UserId ?? string.Empty,
                    Name = store.Name ?? string.Empty,
                    LatinName = store.LatinName ?? string.Empty,
                    Mobile = store.Mobile ?? string.Empty,
                    Logo = store.Logo ?? string.Empty,
                    Address = store.Address ?? string.Empty,
                    IsActive = store.IsActive
                });
            }
            
            return response;
        }
        
        #region Helper Methods
        
        private ProductDto MapToProductDto(Shop.Application.DTOs.Product.ProductDto product)
        {
            if (product == null) return null;
            
            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name ?? string.Empty,
                LatinName = product.LatinName ?? string.Empty,
                Code = product.Code ?? string.Empty,
                Price = product.Price,
                SalePrice = product.SalePrice,
                DiscountPrice = product.DiscountPrice,
                BasePrice = product.BasePrice,
                StockStatus = product.StockStatus,
                Quantity = product.Quantity,
                MaximumSaleInOrder = product.MaximumSaleInOrder,
                Score = product.Score,
                Description = product.Description ?? string.Empty,
                ShortDescription = product.ShortDescription ?? string.Empty,
                ShortLink = product.ShortLink ?? string.Empty,
                TypeId = product.TypeId,
                TypeName = product.TypeName ?? string.Empty,
                VariationName = product.VariationName ?? string.Empty,
                StoreId = product.StoreId,
                StoreName = product.StoreName ?? string.Empty,
                LatinStoreName = product.LatinStoreName ?? string.Empty,
                Image = product.Image ?? string.Empty,
                ParentId = product.ParentId,
                ParentProductName = product.ParentProductName ?? string.Empty,
                BrandId = product.BrandId,
                BrandName = product.BrandName ?? string.Empty,
                LatinBrandName = product.LatinBrandName ?? string.Empty,
                TaxId = product.TaxId,
                RepId = product.RepId,
                RepName = product.RepName ?? string.Empty,
                CreateDateShamsi = product.CreateDateShamsi ?? string.Empty,
                Visit = product.Visit,
                CateguryId = product.CateguryId,
                CateguryName = product.CateguryName ?? string.Empty,
                CateguryLatinName = product.CateguryLatinName ?? string.Empty,
                CountSale = product.CountSale,
                SingleSale = product.SingleSale,
                QuantityPerBundle = product.QuantityPerBundle,
                Keywords = product.Keywords ?? string.Empty,
                IsActive = product.IsActive,
                ShowInDiscountPanels = product.ShowInDiscountPanels
            };
            
            if (product.DiscountDate.HasValue)
            {
                productDto.DiscountDate = Timestamp.FromDateTime(product.DiscountDate.Value.ToUniversalTime());
            }
            
            if (product.CreateDate.HasValue)
            {
                productDto.CreateDate = Timestamp.FromDateTime(product.CreateDate.Value.ToUniversalTime());
            }
            
            return productDto;
        }
        
        private Shop.Application.DTOs.Product.ProductDto MapFromProductDto(ProductDto product)
        {
            var productDto = new Shop.Application.DTOs.Product.ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                LatinName = product.LatinName,
                Code = product.Code,
                Price = product.Price,
                SalePrice = product.SalePrice,
                DiscountPrice = product.DiscountPrice,
                BasePrice = product.BasePrice,
                StockStatus = product.StockStatus,
                Quantity = product.Quantity,
                MaximumSaleInOrder = product.MaximumSaleInOrder,
                Score = product.Score,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                ShortLink = product.ShortLink,
                TypeId = product.TypeId,
                TypeName = product.TypeName,
                VariationName = product.VariationName,
                StoreId = product.StoreId,
                StoreName = product.StoreName,
                LatinStoreName = product.LatinStoreName,
                Image = product.Image,
                ParentId = product.ParentId,
                ParentProductName = product.ParentProductName,
                BrandId = product.BrandId,
                BrandName = product.BrandName,
                LatinBrandName = product.LatinBrandName,
                TaxId = product.TaxId,
                RepId = product.RepId,
                RepName = product.RepName,
                CreateDateShamsi = product.CreateDateShamsi,
                Visit = product.Visit,
                CateguryId = product.CateguryId,
                CateguryName = product.CateguryName,
                CateguryLatinName = product.CateguryLatinName,
                CountSale = product.CountSale,
                SingleSale = product.SingleSale,
                QuantityPerBundle = product.QuantityPerBundle,
                Keywords = product.Keywords,
                IsActive = product.IsActive,
                ShowInDiscountPanels = product.ShowInDiscountPanels
            };
            
            if (product.DiscountDate != null)
            {
                productDto.DiscountDate = product.DiscountDate.ToDateTime();
            }
            
            if (product.CreateDate != null)
            {
                productDto.CreateDate = product.CreateDate.ToDateTime();
            }
            
            return productDto;
        }
        
        #endregion
    }
} 