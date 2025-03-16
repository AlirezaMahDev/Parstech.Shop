using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Shared.Protos.ProductAdmin;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductCategury;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.ProductRepresentation;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.ProductType;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.Tax;
using Shop.Application.DTOs.UserStore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Pages.Admin.Products
{
    [Authorize(Roles = "SupperUser,Sale,Store")]
    public class IndexModel : PageModel
    {
        #region Constructor

        private readonly IMapper _mapper;
        private readonly IProductAdminGrpcClient _productAdminClient;

        public IndexModel(IMapper mapper, IProductAdminGrpcClient productAdminClient)
        {
            _mapper = mapper;
            _productAdminClient = productAdminClient;
        }

        #endregion

        #region Properties

        //paging parameter
        [BindProperty]
        public ProductParameterDto Parameter { get; set; } = new ProductParameterDto();


        //id
        [BindProperty]
        public int productId { get; set; }

        //products
        [BindProperty]
        public ProductPageingDto List { get; set; }

        //product
        [BindProperty]
        public ProductQuickEditDto ProductQuickEditDto { get; set; }

        [BindProperty]
        public ProductDto ProductDto { get; set; }

        //result
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();


        //gallery
        [BindProperty]
        public ProductGalleryDto Gallery { get; set; }

        [BindProperty]
        public List<ProductGalleryDto> Galleries { get; set; }

        //property
        [BindProperty]
        public ProductPropertyDto property { get; set; }

        [BindProperty]
        public List<ProductPropertyDto> properties { get; set; }

        //categury
        [BindProperty]
        public ProductCateguryDto categury { get; set; }

        [BindProperty]
        public List<ProductCateguryDto> categuries { get; set; }

        [BindProperty]
        public string FilterCat { get; set; }

        //rep
        [BindProperty]
        public ProductRepresentationDto rep { get; set; }


        [BindProperty]
        public int Type { get; set; }

        [BindProperty]
        public string Filter { get; set; }


        public List<ProductTypeDto> ProducyTypes { get; set; }
        public List<TaxDto> TaxList { get; set; }
        public List<BrandDto> Brands { get; set; }
        public List<UserStoreDto> UserStores { get; set; }
        #endregion

        #region Get

        public async Task<IActionResult> OnGet()
        {
            // Use gRPC client to get product types, taxes, brands, and stores
            var typesResponse = await _productAdminClient.GetProductTypesAsync();
            ProducyTypes = typesResponse.Types.Select(t => new ProductTypeDto 
            { 
                Id = t.Id, 
                Name = t.Name 
            }).ToList();
            
            var taxesResponse = await _productAdminClient.GetTaxesAsync();
            TaxList = taxesResponse.Taxes.Select(t => new TaxDto 
            { 
                Id = t.Id, 
                Title = t.Title,
                Percent = t.Percent,
                Code = t.Code
            }).ToList();
            
            var brandsResponse = await _productAdminClient.GetBrandsAsync();
            Brands = brandsResponse.Brands.Select(b => new BrandDto 
            { 
                Id = b.Id, 
                Name = b.Name,
                LatinName = b.LatinName,
                IsActive = b.IsActive,
                Logo = b.Logo
            }).ToList();
            
            var storesResponse = await _productAdminClient.GetUserStoresAsync();
            UserStores = storesResponse.Stores.Select(s => new UserStoreDto 
            { 
                Id = s.Id, 
                Name = s.Name,
                LatinName = s.LatinName,
                UserId = s.UserId,
                Mobile = s.Mobile,
                Logo = s.Logo,
                Address = s.Address,
                IsActive = s.IsActive
            }).ToList();
            
            return Page();
        }

        public async Task<IActionResult> OnPostGetData()
        {
            if (Parameter.CurrentPage == 0)
            {
                Parameter.CurrentPage = 1;
            }
            
            Parameter.TakePage = 30;
            
            // Use gRPC client to get products
            var productsResponse = await _productAdminClient.GetProductsForAdminAsync(
                Parameter.CurrentPage, 
                Parameter.TakePage, 
                Parameter.SearchKey, 
                Parameter.FilterCat, 
                Parameter.Filter);
            
            // Map the gRPC response to the application DTO
            List = MapFromProductPageingDto(productsResponse);
            
            if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
            {
                Response.Object2 = "Store";
            }
            else
            {
                Response.Object2 = "All";
            }

            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
        #region Search Paging

        public async Task<IActionResult> OnPostSearch()
        {
            Parameter.TakePage = 30;
            
            // Use gRPC client to search products
            var productsResponse = await _productAdminClient.GetProductsForAdminAsync(
                Parameter.CurrentPage, 
                Parameter.TakePage, 
                Parameter.SearchKey, 
                Parameter.FilterCat, 
                Parameter.Filter);
            
            // Map the gRPC response to the application DTO
            List = MapFromProductPageingDto(productsResponse);
            
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostPaging()
        {
            Parameter.TakePage = 30;
            
            // Use gRPC client for paging
            var productsResponse = await _productAdminClient.GetProductsForAdminAsync(
                Parameter.CurrentPage, 
                Parameter.TakePage, 
                Parameter.SearchKey, 
                Parameter.FilterCat, 
                Parameter.Filter);
            
            // Map the gRPC response to the application DTO
            List = MapFromProductPageingDto(productsResponse);
            
            Response.Object = List;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion


        #region EditProduct

        public async Task<IActionResult> OnPostProduct()
        {
            // Use gRPC client to get product details
            var productResponse = await _productAdminClient.GetProductAsync(productId);
            
            // Map the gRPC response to the application DTO
            ProductDto = MapFromProductDto(productResponse);
            
            Response.Object = ProductDto;
            return new JsonResult(Response);
        }

        public async Task<IActionResult> OnPostEditOrCreateProduct()
        {
            if (ProductDto.Id != 0)
            {
                // Map application DTO to gRPC DTO
                var productGrpc = MapToProductGrpcDto(ProductDto);
                
                // Use gRPC client for quick edit
                var quickEditDto = _mapper.Map<Parstech.Shop.Shared.Protos.ProductAdmin.ProductQuickEditDto>(productGrpc);
                var response = await _productAdminClient.UpdateProductQuickEditAsync(quickEditDto);
                
                Response.Object = ProductQuickEditDto;
                Response.IsSuccessed = response.IsSuccessed;
                Response.Message = response.Message;
                return new JsonResult(Response);
            }
            else
            {
                // Map application DTO to gRPC DTO
                var productGrpc = MapToProductGrpcDto(ProductDto);
                
                // Use gRPC client to create product
                var response = await _productAdminClient.CreateProductAsync(productGrpc);
                
                Response.Object = ProductDto;
                Response.IsSuccessed = response.IsSuccessed;
                Response.Message = response.Message;
                return new JsonResult(Response);
            }
        }
        
        public async Task<IActionResult> OnPostDuplicateForStoreProduct(int productId, int storeId)
        {
            // Use gRPC client to duplicate product for store
            var response = await _productAdminClient.DuplicateProductForStoreAsync(productId, storeId);
            
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            return new JsonResult(Response);
        }
        
        public async Task<IActionResult> OnPostDuplicateProduct(int productId)
        {
            // Use gRPC client to duplicate product
            var response = await _productAdminClient.DuplicateProductAsync(productId);
            
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            return new JsonResult(Response);
        }
        #endregion


        #region Gallery

        public async Task<IActionResult> OnPostGalleries()
        {
            // Use gRPC client to get galleries
            var galleriesResponse = await _productAdminClient.GetGalleriesOfProductAsync(productId);
            
            // Map to application DTO
            Galleries = galleriesResponse.Galleries.Select(g => new ProductGalleryDto
            {
                Id = g.Id,
                ProductId = g.ProductId,
                Title = g.Title,
                Image = g.Image,
                Order = g.Order
            }).ToList();
            
            Response.Object = Galleries;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        // Add similar conversions for other methods...
        
        #region Mapping Methods
        
        private ProductPageingDto MapFromProductPageingDto(Parstech.Shop.Shared.Protos.ProductAdmin.ProductPageingDto dto)
        {
            var result = new ProductPageingDto
            {
                CurrentPage = dto.CurrentPage,
                PageCount = dto.PageCount,
                RowCount = dto.RowCount,
                List = dto.List.Select(p => MapFromProductDto(p)).ToList()
            };
            
            return result;
        }
        
        private ProductDto MapFromProductDto(Parstech.Shop.Shared.Protos.ProductAdmin.ProductDto dto)
        {
            var product = new ProductDto
            {
                Id = dto.Id,
                Name = dto.Name,
                LatinName = dto.LatinName,
                Code = dto.Code,
                Price = dto.Price,
                SalePrice = dto.SalePrice,
                DiscountPrice = dto.DiscountPrice,
                BasePrice = dto.BasePrice,
                StockStatus = dto.StockStatus,
                Quantity = dto.Quantity,
                MaximumSaleInOrder = dto.MaximumSaleInOrder,
                Score = dto.Score,
                Description = dto.Description,
                ShortDescription = dto.ShortDescription,
                ShortLink = dto.ShortLink,
                TypeId = dto.TypeId,
                TypeName = dto.TypeName,
                VariationName = dto.VariationName,
                StoreId = dto.StoreId,
                StoreName = dto.StoreName,
                LatinStoreName = dto.LatinStoreName,
                Image = dto.Image,
                ParentId = dto.ParentId,
                ParentProductName = dto.ParentProductName,
                BrandId = dto.BrandId,
                BrandName = dto.BrandName,
                LatinBrandName = dto.LatinBrandName,
                TaxId = dto.TaxId,
                RepId = dto.RepId,
                RepName = dto.RepName,
                CreateDateShamsi = dto.CreateDateShamsi,
                Visit = dto.Visit,
                CateguryId = dto.CateguryId,
                CateguryName = dto.CateguryName,
                CateguryLatinName = dto.CateguryLatinName,
                CountSale = dto.CountSale,
                SingleSale = dto.SingleSale,
                QuantityPerBundle = dto.QuantityPerBundle,
                Keywords = dto.Keywords,
                IsActive = dto.IsActive,
                ShowInDiscountPanels = dto.ShowInDiscountPanels
            };
            
            if (dto.DiscountDate != null)
            {
                product.DiscountDate = dto.DiscountDate.ToDateTime();
            }
            
            if (dto.CreateDate != null)
            {
                product.CreateDate = dto.CreateDate.ToDateTime();
            }
            
            return product;
        }
        
        private Parstech.Shop.Shared.Protos.ProductAdmin.ProductDto MapToProductGrpcDto(ProductDto product)
        {
            var dto = new Parstech.Shop.Shared.Protos.ProductAdmin.ProductDto
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
                dto.DiscountDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(product.DiscountDate.Value.ToUniversalTime());
            }
            
            if (product.CreateDate.HasValue)
            {
                dto.CreateDate = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(product.CreateDate.Value.ToUniversalTime());
            }
            
            return dto;
        }
        
        #endregion

        #region Rep

        public async Task<IActionResult> OnPostProductRep()
        {
            var representations = await _productAdminClient.GetProductRepresentationsAsync(productId);
            rep = representations.FirstOrDefault(); // Get the first representation if exists
            Response.Object = rep;
            return new JsonResult(Response);
        }

        #endregion
        #region Categury

        public async Task<IActionResult> OnPostCateguries()
        {
            var categoriesResponse = await _productAdminClient.GetProductCategoriesAsync(productId);
            categuries = categoriesResponse.Categories.Select(c => new ProductCateguryDto
            {
                Id = c.Id,
                ProductId = c.ProductId,
                CateguryId = c.CategoryId,
                CateguryName = c.CategoryName
            }).ToList();
            
            Response.Object = categuries;
            return new JsonResult(Response);
        }
        public async Task<IActionResult> OnPostCategury()
        {
            var categoriesResponse = await _productAdminClient.GetProductCategoriesAsync(productId);
            categury = categoriesResponse.Categories.Select(c => new ProductCateguryDto
            {
                Id = c.Id,
                ProductId = c.ProductId,
                CateguryId = c.CategoryId,
                CateguryName = c.CategoryName
            }).FirstOrDefault() ?? new ProductCateguryDto();
            
            Response.Object = categury;
            return new JsonResult(Response);
        }
        public async Task<IActionResult> OnPostDeleteCategury()
        {
            var response = await _productAdminClient.DeleteProductCategoryAsync(productId);
            
            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            Response.Object = productId;
            return new JsonResult(Response);
        }


        public async Task<IActionResult> OnPostGetAllCateguries()
        {
            var categoriesResponse = await _productAdminClient.GetCategoriesAsync();
            var categuries = categoriesResponse.Categories.Select(c => new CateguryDto
            {
                GroupId = c.Id,
                GroupTitle = c.Title,
                LatinGroupTitle = c.LatinTitle,
                ParentId = c.ParentId,
                Image = c.Image,
                IsParnet = c.IsParent,
                Show = c.Show
            }).ToList();
            
            if (!string.IsNullOrEmpty(FilterCat))
            {
                categuries = categuries.Where(c => c.GroupTitle.Contains(FilterCat)).ToList();
            }
            
            Response.Object = categuries;
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }
        public async Task<IActionResult> OnPostEditOrCreateCategury()
        {
            if (categury.Id != 0)
            {
                var request = new Parstech.Shop.Shared.Protos.ProductAdmin.ProductCategoryDto
                {
                    Id = categury.Id,
                    ProductId = categury.ProductId,
                    CategoryId = categury.CateguryId,
                    CategoryName = categury.CateguryName ?? string.Empty
                };
                
                var response = await _productAdminClient.UpdateProductCategoryAsync(request);
                
                Response.IsSuccessed = response.IsSuccessed;
                Response.Message = "دسته بندی محصول با موفقیت ویرایش شد";
                Response.Object = categury;
                return new JsonResult(Response);
            }
            else
            {
                var request = new Parstech.Shop.Shared.Protos.ProductAdmin.ProductCategoryDto
                {
                    ProductId = categury.ProductId,
                    CategoryId = categury.CateguryId,
                    CategoryName = categury.CateguryName ?? string.Empty
                };
                
                var response = await _productAdminClient.AddProductCategoryAsync(request);
                
                Response.IsSuccessed = response.IsSuccessed;
                Response.Message = "دسته بندی محصول با موفقیت ثبت شد";
                Response.Object = categury;
                return new JsonResult(Response);
            }
        }

        #endregion

        #region Property

        public async Task<IActionResult> OnPostProperties()
        {
            var propertiesResponse = await _productAdminClient.GetProductPropertiesAsync(productId);
            properties = propertiesResponse.Properties.Select(p => new ProductPropertyDto
            {
                Id = p.Id,
                ProductId = p.ProductId,
                PropertyId = p.PropertyId,
                Value = p.Value,
                IsFilter = p.IsFilter,
                IsShow = p.IsShow,
                PropertyName = p.PropertyName
            }).ToList();
            
            Response.Object = properties;
            return new JsonResult(Response);
        }
        public async Task<IActionResult> OnPostProperty()
        {
            var propertiesResponse = await _productAdminClient.GetProductPropertiesAsync(productId);
            property = propertiesResponse.Properties.Select(p => new ProductPropertyDto
            {
                Id = p.Id,
                ProductId = p.ProductId,
                PropertyId = p.PropertyId,
                Value = p.Value,
                IsFilter = p.IsFilter,
                IsShow = p.IsShow,
                PropertyName = p.PropertyName
            }).FirstOrDefault() ?? new ProductPropertyDto();
            
            Response.Object = property;
            return new JsonResult(Response);
        }
        public async Task<IActionResult> OnPostEditOrCreateProperty()
        {
            if (property.Id != 0)
            {
                var request = new Parstech.Shop.Shared.Protos.ProductAdmin.ProductPropertyDto
                {
                    Id = property.Id,
                    ProductId = property.ProductId,
                    PropertyId = property.PropertyId,
                    Value = property.Value ?? string.Empty,
                    IsFilter = property.IsFilter,
                    IsShow = property.IsShow,
                    PropertyName = property.PropertyName ?? string.Empty
                };
                
                var response = await _productAdminClient.UpdateProductPropertyAsync(request);
                
                Response.IsSuccessed = response.IsSuccessed;
                Response.Message = "ویژگی محصول با موفقیت ویرایش شد";
                Response.Object = property;
                return new JsonResult(Response);
            }
            else
            {
                var request = new Parstech.Shop.Shared.Protos.ProductAdmin.ProductPropertyDto
                {
                    ProductId = property.ProductId,
                    PropertyId = property.PropertyId,
                    Value = property.Value ?? string.Empty,
                    IsFilter = property.IsFilter,
                    IsShow = property.IsShow,
                    PropertyName = property.PropertyName ?? string.Empty
                };
                
                var response = await _productAdminClient.AddProductPropertyAsync(request);
                
                Response.IsSuccessed = response.IsSuccessed;
                Response.Message = "ویژگی محصول با موفقیت ثبت شد";
                Response.Object = property;
                return new JsonResult(Response);
            }
        }

        #endregion

        public async Task<IActionResult> OnPostProductParents()
        {
            if (Type == 1)
            {
                var response = await _productAdminClient.GetAllParentVariableProductsAsync(Filter);
                Response.Object = response.Products.Select(p => MapFromProductDto(p)).ToList();
            }
            else if (Type == 2)
            {
                var response = await _productAdminClient.GetAllParentBundleProductsAsync(Filter);
                Response.Object = response.Products.Select(p => MapFromProductDto(p)).ToList();
            }

            return new JsonResult(Response);
        }

        #region Delete
        public async Task<IActionResult> OnPostDelete()
        {
            var response = await _productAdminClient.DeleteProductAsync(productId);

            Response.IsSuccessed = response.IsSuccessed;
            Response.Message = response.Message;
            return new JsonResult(Response);
        }
        #endregion

        #region Add product For Store

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostSearchProduct(string Filter)
        {
            var response = await _productAdminClient.SearchProductsAsync(Filter, 30);
            Response.Object = response.Products.Select(p => MapFromProductDto(p)).ToList();
            Response.IsSuccessed = true;
            return new JsonResult(Response);
        }

        #endregion
        #region Get Product Stores

        [ValidateAntiForgeryToken]
        public async Task<JsonResult> OnPostProductStores(int ProductId)
        {
            if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
            {
                var list = new List<ProductStockPriceStoreDto>();
                var item = new ProductStockPriceStoreDto()
                {
                    Id = 0,
                    StoreName = "دسترسی ندارید"
                };
                list.Add(item);
                Response.Object = list;
                Response.IsSuccessed = true;
            }
            else
            {
                var response = await _productAdminClient.GetProductStockPriceStoresAsync(ProductId);
                var list = response.Stores.Select(s => new ProductStockPriceStoreDto
                {
                    Id = s.Id,
                    ProductId = s.ProductId,
                    StoreId = s.StoreId,
                    StoreName = s.StoreName
                }).ToList();
                
                Response.Object = list;
                Response.IsSuccessed = true;
            }

            return new JsonResult(Response);
        }

        #endregion

    }
}
