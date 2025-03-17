using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Globalization;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Products;

[Authorize(Roles = "SupperUser,Sale,Store")]
public class CreateOrUpdateModel : PageModel
{
    #region Constractor

    private readonly ProductDetailAdminGrpcClient _productDetailClient;
    private readonly UserGrpcClient _userClient;
    private readonly IMapper _mapper;
    private readonly IProductStockPriceRepository _productStockRep;

    public CreateOrUpdateModel(
        ProductDetailAdminGrpcClient productDetailClient,
        UserGrpcClient userClient,
        IMapper mapper,
        IProductStockPriceRepository productStockRep)
    {
        _productDetailClient = productDetailClient;
        _userClient = userClient;
        _mapper = mapper;
        _productStockRep = productStockRep;
    }

    #endregion

    #region Properties

    //id
    [BindProperty]
    public int? productId { get; set; }


    //product
    [BindProperty]
    public ProductDto ProductDto { get; set; }

    //gallery
    [BindProperty]
    public ProductGalleryDto Gallery { get; set; }


    //[BindProperty] public List<ProductGalleryDto> Galleries { get; set; }

    //property
    [BindProperty]
    public string? productContent { get; set; }


    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    //result
    public class Object
    {
        public ProductDto ProductDto { get; set; }

        //public List<ProductGalleryDto> GalleryDtos { get; set; }
        //public List<ProductPropertyDto> PropertyDtos { get; set; }
        //public List<ProductCateguryDto> CateguryDtos { get; set; }
        //public ChildsAndStock ChildsAndStock { get; set; }
        public string Role { get; set; } = "";
    }

    [BindProperty]
    public Object ResultObject { get; set; } = new();


    //Categuries

    public class CateguryObject
    {
        public List<CateguryDto> categuries { get; set; }

        public List<CateguryDto> subCateguries { get; set; } = new();

        //properties
        public List<PropertyCateguryDto> propertyCategury { get; set; }
    }

    [BindProperty]
    public CateguryObject categuryObject { get; set; } = new();


    [BindProperty]
    public int categuryId { get; set; }


    //feutures
    [BindProperty]
    public List<PropertyDto> Properties { get; set; } = new();


    [BindProperty]
    public int CatId { get; set; }

    [BindProperty]
    public int propertyCatId { get; set; }

    [BindProperty]
    public string propertyFilter { get; set; }


    [BindProperty]
    public ProductPropertyDto AddFeutureInput { get; set; }

    public List<ProductTypeDto> ProducyTypes { get; set; }
    public List<TaxDto> TaxList { get; set; }
    public List<BrandDto> Brands { get; set; }

    [BindProperty]
    public int Type { get; set; }

    [BindProperty]
    public string Filter { get; set; }


    //categury
    [BindProperty]
    public ProductCateguryDto categury { get; set; }

    [BindProperty]
    public List<ProductCateguryDto> categuries { get; set; }

    [BindProperty]
    public string FilterCat { get; set; }

    [BindProperty]
    public ProductStockPriceDto productStock { get; set; }

    [BindProperty]
    public ProductRepresentationDto productRepresentation { get; set; }


    [BindProperty]
    public ProductRepresentationDto ProductRepresentationDto { get; set; }

    [BindProperty]
    public List<RepresentationTypeDto> repTypes { get; set; }


    [BindProperty]
    public ProductDto variationDto { get; set; }

    public List<UserStoreDto> UserStores { get; set; } = new();

    [BindProperty]
    public int SelectedBrand { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet(int? Id)
    {
        productId = Id;
        categuryObject.categuries = await _productDetailClient.GetCategoriesAsync();
        categuryObject.propertyCategury = await _productDetailClient.GetPropertyCategoriesAsync();

        ProducyTypes = await _productDetailClient.GetProductTypesAsync();
        TaxList = await _productDetailClient.GetTaxesAsync();
        Brands = await _productDetailClient.GetBrandsAsync();

        if (productId != null)
        {
            var product = await _productDetailClient.GetProductByIdAsync(productId.Value);
            productContent = product.Description;
            SelectedBrand = product.BrandId;
        }

        repTypes = await _productDetailClient.GetRepresentationTypesAsync();

        if (User.IsInRole("SupperUser"))
        {
            UserStores = await _productDetailClient.GetUserStoresAsync();
        }
        else if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            var user = await _userClient.GetUserByUserNameAsync(User.Identity.Name);
            UserStores = await _productDetailClient.GetUserStoresAsync();
            UserStores = UserStores.Where(s => s.UserId == user.Id).ToList();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        if (productId != null)
        {
            ResultObject.ProductDto = await _productDetailClient.GetProductByIdAsync(productId.Value);
            productContent = ResultObject.ProductDto.Description;

            Response.Object = ResultObject;
            Response.IsSuccessed = true;
        }

        if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            Response.Object2 = "Store";
        }
        else
        {
            Response.Object2 = "All";
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Save

    public async Task<IActionResult> OnPostSave(int isActive)
    {
        if (User.IsInRole("Store"))
        {
            ProductDto.IsActive = false;
        }
        else
        {
            if (isActive == 1)
            {
                ProductDto.IsActive = true;
            }
            else
            {
                ProductDto.IsActive = false;
            }
        }

        if (ProductDto.Id != 0)
        {
            var response = await _productDetailClient.UpdateProductAsync(ProductDto);
            Response = response;
            Response.Object2 = "Update";
        }
        else
        {
            var response = await _productDetailClient.CreateProductAsync(ProductDto);
            Response = response;
            Response.Object2 = "Create";
        }

        return new JsonResult(Response);
    }

    #endregion

    #region Categury

    public async Task<IActionResult> OnPostGetAllCateguries()
    {
        var categories = await _productDetailClient.GetCategoriesAsync();
        if (!string.IsNullOrEmpty(FilterCat))
        {
            categories = categories.Where(c => c.GroupTitle.Contains(FilterCat)).ToList();
        }

        Response.Object = categories;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreateCategury()
    {
        var response = await _productDetailClient.AddProductCategoryAsync(categury);
        Response = response;
        Response.Object = categury;
        Response.Message = "دسته بندی محصول با موفقیت ثبت شد";

        #region NotActive Product When Role Is Store

        if (User.IsInRole("Store"))
        {
            var currtentProduct = await _productDetailClient.GetProductByIdAsync(categury.ProductId);
            currtentProduct.IsActive = false;
            await _productDetailClient.UpdateProductAsync(currtentProduct);
        }

        #endregion

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteCategury()
    {
        var response = await _productDetailClient.DeleteProductCategoryAsync(productId.Value);
        Response = response;
        return new JsonResult(Response);
    }

    #endregion

    #region Feuture

    public async Task<IActionResult> OnPostSubs(int parentId)
    {
        var subCategories = await _productDetailClient.GetSubCategoriesAsync(parentId);

        Response.Object = subCategories;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostSearchFeuture()
    {
        Properties = await _productDetailClient.GetPropertiesAsync(propertyFilter, propertyCatId);

        Response.Object = Properties;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostAddEditFeuture()
    {
        ResponseDto response;

        if (AddFeutureInput.Id != 0)
        {
            response = await _productDetailClient.UpdateProductPropertyAsync(AddFeutureInput);
        }
        else
        {
            response = await _productDetailClient.AddProductPropertyAsync(AddFeutureInput);
        }

        Response = response;

        #region NotActive Product When Role Is Store

        if (User.IsInRole("Store"))
        {
            var currtentProduct = await _productDetailClient.GetProductByIdAsync(Gallery.ProductId);
            currtentProduct.IsActive = false;
            await _productDetailClient.UpdateProductAsync(currtentProduct);
        }

        #endregion

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteFeuture()
    {
        if (AddFeutureInput.Id != 0)
        {
            var response = await _productDetailClient.DeleteProductPropertyAsync(AddFeutureInput.Id);
            Response = response;

            #region NotActive Product When Role Is Store

            if (User.IsInRole("Store"))
            {
                var currtentProduct = await _productDetailClient.GetProductByIdAsync(Gallery.ProductId);
                currtentProduct.IsActive = false;
                await _productDetailClient.UpdateProductAsync(currtentProduct);
            }

            #endregion

            return new JsonResult(Response);
        }
        else
        {
            Response.IsSuccessed = false;
            return new JsonResult(Response);
        }
    }

    #endregion

    #region Variation

    public async Task<IActionResult> OnPostAddVariation()
    {
        variationDto.ParentId = productId.Value;
        var response = await _productDetailClient.AddProductVariationAsync(variationDto);
        Response = response;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditVariation(int variationId)
    {
        var variation = await _productDetailClient.GetProductVariationAsync(variationId);
        variation.VariationName = variationDto.VariationName;
        var response = await _productDetailClient.UpdateProductAsync(variation);
        Response = response;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostAddSingleStock(int storeId)
    {
        // This method duplicates a product for a store, we need to modify it to use gRPC
        // Since this is a complex operation, we might need to add a specific method to the gRPC service
        // For now, we'll return a not implemented response
        Response.IsSuccessed = false;
        Response.Message = "This operation is not yet implemented in the gRPC service";
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteChild(int childId)
    {
        var response = await _productDetailClient.DeleteProductVariationAsync(childId);
        Response = response;
        return new JsonResult(Response);
    }

    #endregion

    #region Stock

    #region Price

    public async Task<IActionResult> OnPostPriceItem()
    {
        var stockPrices = await _productDetailClient.GetProductStockPricesAsync(productId.Value);
        productStock = stockPrices.FirstOrDefault();

        Response.Object = productStock;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditPriceItem()
    {
        var stockPrices = await _productDetailClient.GetProductStockPricesAsync(productId.Value);
        var currentproductStock = stockPrices.FirstOrDefault(sp => sp.Id == productStock.Id);

        productStock.Price = long.Parse(productStock.TextPrice.Replace(",", ""));
        productStock.SalePrice = long.Parse(productStock.TextSalePrice.Replace(",", ""));
        productStock.DiscountPrice = long.Parse(productStock.TextDiscountPrice.Replace(",", ""));
        productStock.BasePrice = long.Parse(productStock.TextBasePrice.Replace(",", ""));
        productStock.Quantity = currentproductStock.Quantity;
        productStock.MaximumSaleInOrder = currentproductStock.MaximumSaleInOrder;
        productStock.StoreId = currentproductStock.StoreId;
        productStock.RepresentationId = currentproductStock.RepresentationId;
        productStock.TaxId = currentproductStock.TaxId;
        productStock.QuantityPerBundle = currentproductStock.QuantityPerBundle;
        productStock.SpecialFromDate = currentproductStock.SpecialFromDate;
        productStock.SpecialToDate = currentproductStock.SpecialToDate;

        #region Validator

        var validator = new ProductPriceValidator();
        var valid = validator.Validate(productStock);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;

            return new JsonResult(Response);
        }

        #endregion

        if (productStock.DiscountPrice > 0)
        {
            if (productStock.DiscountDateShamsi != null)
            {
                var Date = ConvertPersianNumbersToEnglish.ToEnglishNumber(productStock.DiscountDateShamsi);

                string[] std = Date.Split('/');
                DateTime az = new(int.Parse(std[0]),
                    int.Parse(std[1]),
                    int.Parse(std[2]),
                    new PersianCalendar()
                );
                productStock.SpecialToDate = az;
            }
        }
        else
        {
            productStock.SpecialToDate = null;
        }

        var response = await _productDetailClient.UpdateProductStockPriceAsync(productStock);
        Response = response;

        return new JsonResult(Response);
    }

    #endregion

    #region Mojudi

    public async Task<IActionResult> OnPostAddProductRepresentation()
    {
        var user = await _userClient.GetUserByUserNameAsync(User.Identity.Name);
        ProductRepresentationDto.UserId = user.Id;
        var response = await _productDetailClient.AddProductRepresentationAsync(ProductRepresentationDto);
        Response = response;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostQuickAddProductRepresentation()
    {
        var user = await _userClient.GetUserByUserNameAsync(User.Identity.Name);
        ProductRepresentationDto.UserId = user.Id;
        var response = await _productDetailClient.QuickAddProductRepresentationAsync(ProductRepresentationDto);
        Response = response;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostChangeQuantityPerBundle(int productStockPriceId, int QuantityPerBundle)
    {
        var response =
            await _productDetailClient.UpdateStockQuantityPerBundleAsync(productStockPriceId, QuantityPerBundle);
        Response = response;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteProductStock(int rep, int id)
    {
        var stockPrices = await _productDetailClient.GetProductStockPricesAsync(productId.Value);
        var pstock = stockPrices.FirstOrDefault(sp => sp.Id == id);

        var response = await _productDetailClient.DeleteProductStockPriceAsync(id);
        Response = response;
        return new JsonResult(Response);
    }

    #endregion

    #endregion
}