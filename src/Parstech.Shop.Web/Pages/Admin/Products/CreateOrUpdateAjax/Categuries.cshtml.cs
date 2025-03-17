using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Products.CreateOrUpdateAjax;

public class CateguriesModel : PageModel
{
    #region Constructor

    private readonly IProductComponentsAdminGrpcClient _productComponentsClient;
    private readonly IProductStockPriceRepository _productStockRep;

    public CateguriesModel(
        IProductComponentsAdminGrpcClient productComponentsClient,
        IProductStockPriceRepository productStockRep)
    {
        _productComponentsClient = productComponentsClient;
        _productStockRep = productStockRep;
    }

    #endregion

    //id
    [BindProperty]
    public int? productId { get; set; }

    //product
    [BindProperty]
    public List<ProductCategoryDto> Categuries { get; set; } = new();

    [BindProperty]
    public ProductCategoryDto categury { get; set; }

    [BindProperty]
    public List<ProductCategoryDto> categuries { get; set; } = new();

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    [BindProperty]
    public string FilterCat { get; set; }

    public async Task OnGet(int? id)
    {
        productId = id;
        if (productId != null)
        {
            var response = await _productComponentsClient.GetCategoriesOfProductAsync(productId.Value);
            if (response.IsSuccess)
            {
                Categuries = response.Categories.ToList();
            }
        }
    }

    #region Category

    public async Task<IActionResult> OnPostGetAllCateguries()
    {
        var response = await _productComponentsClient.GetAllCategoriesAsync(FilterCat);
        if (response.IsSuccess)
        {
            Response.Object = response.Categories;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "Failed to retrieve categories";
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostCateguries()
    {
        var response = await _productComponentsClient.GetCategoriesOfProductAsync(productId.Value);
        if (response.IsSuccess)
        {
            categuries = response.Categories.ToList();
            Response.Object = categuries;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "Failed to retrieve product categories";
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostCategury()
    {
        var response = await _productComponentsClient.GetCategoryOfProductAsync(productId.Value);
        if (response.IsSuccess)
        {
            categury = response.Category;
            Response.Object = categury;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "Failed to retrieve product category";
        }

        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteCategury()
    {
        var response = await _productComponentsClient.DeleteProductCategoryAsync(productId.Value);
        if (response.IsSuccess)
        {
            Response.Object = response.ProductId;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "Failed to delete product category";
        }

        return new JsonResult(Response);
    }

    #endregion
}