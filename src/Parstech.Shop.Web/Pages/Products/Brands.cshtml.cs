using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Products;

public class BrandsModel : PageModel
{
    #region Constructor

    private readonly BrandGrpcClient _brandClient;
    private readonly CategoryGrpcClient _categoryClient;
    private readonly ProductGrpcClient _productClient;
    private readonly UserStoreGrpcClient _userStoreClient;

    public BrandsModel(
        BrandGrpcClient brandClient,
        CategoryGrpcClient categoryClient,
        ProductGrpcClient productClient,
        UserStoreGrpcClient userStoreClient)
    {
        _brandClient = brandClient;
        _categoryClient = categoryClient;
        _productClient = productClient;
        _userStoreClient = userStoreClient;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ProductParameter Parameter { get; set; } = new ProductParameter();

    //products
    [BindProperty]
    public ProductPaging List { get; set; }

    //result
    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    //brand
    [BindProperty]
    public string Brand { get; set; }

    [BindProperty]
    public string FilterCat { get; set; }

    [BindProperty]
    public int Type { get; set; }

    [BindProperty]
    public string Filter { get; set; }

    public List<Category> Categories { get; set; } = new();
    public List<Brand> Brands { get; set; } = new();
    public List<UserStore> Stores { get; set; } = new();

    #endregion

    #region Get

    public async Task<IActionResult> OnGet(string brand)
    {
        Brand = brand;
        var brandsResponse = await _brandClient.GetAllBrandsAsync();
        Brands = brandsResponse.Brands.ToList();
        Stores = (await _userStoreClient.GetStoresAsync()).ToList();

        // Get categories in a hierarchical structure
        Categories = new();
        var parentCategories = await _categoryClient.GetParentCategoriesAsync();
        foreach (var parent in parentCategories)
        {
            Categories.Add(parent);
            var subParents = await _categoryClient.GetSubCategoriesAsync(parent.Id);
            foreach (var subParent in subParents)
            {
                Categories.Add(subParent);
                var subCategories = await _categoryClient.GetSubCategoriesAsync(subParent.Id);
                foreach (var subCategory in subCategories)
                {
                    Categories.Add(subCategory);
                }
            }
        }

        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        Parameter.PageSize = 30;
        var productRequest = new ProductPagingRequest
        {
            Parameter = Parameter, UserName = User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty
        };

        List = await _productClient.GetProductsAsync(Parameter,
            User.Identity.IsAuthenticated ? User.Identity.Name : null);
        Response.Object = List;
        var category = await _categoryClient.GetCategoryByLatinNameAsync(Parameter.Brand);
        Response.Object2 = category;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion
}