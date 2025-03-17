using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.GrpcClients;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Products.CreateOrUpdateAjax;

public class ChildsAndStockModel : PageModel
{
    #region Constructor

    private readonly IProductComponentsAdminGrpcClient _productComponentsClient;
    private readonly IProductAdminGrpcClient _productAdminClient;
    private readonly IUserAdminGrpcClient _userAdminClient;
    private readonly IProductStockPriceRepository _productStockRep;

    public ChildsAndStockModel(
        IProductComponentsAdminGrpcClient productComponentsClient,
        IProductAdminGrpcClient productAdminClient,
        IUserAdminGrpcClient userAdminClient,
        IProductStockPriceRepository productStockRep)
    {
        _productComponentsClient = productComponentsClient;
        _productAdminClient = productAdminClient;
        _userAdminClient = userAdminClient;
        _productStockRep = productStockRep;
    }

    #endregion

    //id
    [BindProperty]
    public int? productId { get; set; }

    [BindProperty]
    public int? TypeId { get; set; } = 0;

    public ChildsAndStocksResponse ChildsAndStock { get; set; }
    public ProductDto product { get; set; }

    public async Task OnGet(int? id)
    {
        productId = id;

        if (productId != 0)
        {
            var productResponse = await _productAdminClient.GetProductAsync(id.Value);
            if (productResponse.IsSuccess)
            {
                product = productResponse.Product;
                TypeId = product.TypeId;
            }
        }

        if (User.IsInRole("Store") || User.IsInRole("StoreBySend"))
        {
            var userResponse = await _userAdminClient.GetUserByUserNameAsync(User.Identity.Name);
            if (userResponse.IsSuccess)
            {
                var userStoreResponse = await _userAdminClient.GetUserStoreOfUserAsync(userResponse.User.Id);
                if (userStoreResponse.IsSuccess)
                {
                    ChildsAndStock = await _productComponentsClient.GetChildsAndProductStocksAsync(
                        productId.Value,
                        userStoreResponse.UserStore.Id);
                }
            }
        }
        else
        {
            ChildsAndStock = await _productComponentsClient.GetChildsAndProductStocksAsync(
                productId.Value,
                0);
        }
    }
}