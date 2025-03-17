using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Products;

[Authorize(Roles = "SupperUser,Sale,Store")]
public class EditContentModel : PageModel
{
    #region Constractor

    private readonly ProductDetailAdminGrpcClient _productDetailClient;

    public EditContentModel(ProductDetailAdminGrpcClient productDetailClient)
    {
        _productDetailClient = productDetailClient;
    }

    #endregion

    #region Properties

    //id
    [BindProperty]
    public int productId { get; set; }


    //product
    [BindProperty]
    public ProductDto ProductDto { get; set; }

    //gallery
    [BindProperty]
    public ProductGalleryDto Gallery { get; set; }


    [BindProperty]
    public int GalleryId { get; set; }

    [BindProperty]
    public List<ProductGalleryDto> Galleries { get; set; }

    //property
    [BindProperty]
    public string? productContent { get; set; }


    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    //result
    public class Object
    {
        public ProductDto ProductDto { get; set; }
        public List<ProductGalleryDto> GalleryDtos { get; set; }
        public List<ProductPropertyDto> PropertyDtos { get; set; }
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
    public ProductPropertyDto AddFeutureInput { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet(int Id)
    {
        productId = Id;
        categuryObject.categuries = await _productDetailClient.GetCategoriesAsync();
        categuryObject.propertyCategury = await _productDetailClient.GetPropertyCategoriesAsync();
        var product = await _productDetailClient.GetProductByIdAsync(productId);
        productContent = product.Description;
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        ResultObject.ProductDto = await _productDetailClient.GetProductByIdAsync(productId);
        ResultObject.GalleryDtos = await _productDetailClient.GetProductGalleryAsync(productId);
        ResultObject.PropertyDtos = await _productDetailClient.GetProductPropertiesAsync(productId);

        Response.Object = ResultObject;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    #endregion

    #region Gallery

    public async Task<IActionResult> OnPostAddGallery()
    {
        #region Validator

        var validator = new ProductGalleryDtoValidator();
        var valid = validator.Validate(Gallery);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Object = Gallery;
            return new JsonResult(Response);
        }

        #endregion

        var response = await _productDetailClient.AddProductGalleryAsync(Gallery);
        Response = response;
        Response.Object = Gallery;
        Response.Message = "تصویر محصول با موفقیت بارگذاری گردید.";

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteGallery()
    {
        var response = await _productDetailClient.DeleteProductGalleryAsync(GalleryId);
        Response = response;
        Response.Message = "تصویر محصول با موفقیت حذف گردید.";
        Response.Object = Gallery;

        return new JsonResult(Response);
    }

    #endregion

    #region Feuture

    public async Task<IActionResult> OnPostSubs(int parentId)
    {
        categuryObject.subCateguries = await _productDetailClient.GetSubCategoriesAsync(parentId);

        Response.Object = categuryObject.subCateguries;
        Response.IsSuccessed = true;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostSearchFeuture()
    {
        Properties = await _productDetailClient.GetPropertiesAsync(null, propertyCatId);

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
        Response.Object = AddFeutureInput;

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteFeuture()
    {
        if (AddFeutureInput.Id != 0)
        {
            var response = await _productDetailClient.DeleteProductPropertyAsync(AddFeutureInput.Id);
            Response = response;

            return new JsonResult(Response);
        }
        else
        {
            Response.IsSuccessed = false;
            return new JsonResult(Response);
        }
    }

    #endregion

    public async Task<IActionResult> OnPostSave()
    {
        // Get the current product
        var product = await _productDetailClient.GetProductByIdAsync(productId);

        // Update the description
        product.Description = productContent;

        // Save the product
        var response = await _productDetailClient.UpdateProductAsync(product);

        Response = response;
        Response.Object = product;
        return new JsonResult(Response);
    }
}