using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Products;

public class StoresModel : PageModel
{
    #region Constructor

    private readonly ProductGrpcClient _productClient;
    private readonly UserStoreGrpcClient _userStoreClient;

    public StoresModel(ProductGrpcClient productClient, UserStoreGrpcClient userStoreClient)
    {
        _productClient = productClient;
        _userStoreClient = userStoreClient;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ProductSearchParameterRequest Parameter { get; set; } = new ProductSearchParameterRequest();

    //products
    [BindProperty]
    public ProductPageing List { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    //category
    [BindProperty]
    public string Store { get; set; }

    [BindProperty]
    public string FilterCat { get; set; }

    [BindProperty]
    public int Type { get; set; }

    [BindProperty]
    public string Filter { get; set; }

    public List<CateguryDto> categuries { get; set; }
    public List<BrandDto> Brands { get; set; }
    public List<UserStoreDto> Stores { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet(string store)
    {
        Store = store;
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostData(int skip, string store)
    {
        Parameter.Skip = skip;
        Parameter.Store = store;
        List = await _productClient.ProductPagingSearchOrStoreAsync(Parameter);
        Response.Object = List;
        var userStore = await _userStoreClient.GetStoreByLatinNameAsync(Parameter.Store);
        Response.Object2 = userStore;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
}