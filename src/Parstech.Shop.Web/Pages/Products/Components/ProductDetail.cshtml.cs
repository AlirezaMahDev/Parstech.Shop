using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.GrpcClients;

namespace Parstech.Shop.Web.Pages.Products.Components;

public class ProductDetailModel : PageModel
{
    #region Constructor

    private readonly IProductComponentsAdminGrpcClient _productComponentsClient;
    private readonly IProductStockPriceRepository _productStockRep;

    public ProductDetailModel(
        IProductComponentsAdminGrpcClient productComponentsClient,
        IProductStockPriceRepository productStockRep)
    {
        _productComponentsClient = productComponentsClient;
        _productStockRep = productStockRep;
    }

    #endregion

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    public async Task<IActionResult> OnGet(string ShortLink, int StoreId)
    {
        #region Get User If Authenticated

        string? userName = "";
        if (User.Identity.IsAuthenticated)
        {
            userName = User.Identity.Name;
        }
        else
        {
            userName = null;
        }

        #endregion

        var productDetail = await _productComponentsClient.GetProductDetailAsync(ShortLink, StoreId, userName);

        if (productDetail.IsSuccess)
        {
            Response.Object = productDetail;
            Response.IsSuccessed = true;
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "Failed to retrieve product details";
        }

        return new JsonResult(Response);
    }
}