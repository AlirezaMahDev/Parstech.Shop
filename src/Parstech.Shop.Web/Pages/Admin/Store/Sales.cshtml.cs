using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Store;

[Authorize(Roles = "SupperUser,Finanicial,Sale,Store")]
public class SalesModel : PageModel
{
    #region Constructor

    private readonly StoreAdminGrpcClient _storeAdminClient;
    private readonly UserGrpcClient _userClient;

    public SalesModel(StoreAdminGrpcClient storeAdminClient, UserGrpcClient userClient)
    {
        _storeAdminClient = storeAdminClient;
        _userClient = userClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public SalesParameterDto parameters { get; set; }

    public SalesPagingDto result { get; set; }
    public List<UserForSelectListDto> Users { get; set; }

    [BindProperty]
    public List<UserStoreDto> UserStores { get; set; } = new();

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    #endregion

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostGetData()
    {
        parameters.CurrentPage = 1;
        parameters.TakePage = 100;
        if (User.IsInRole("SupperUser"))
        {
            result = await _storeAdminClient.GetSalesForStoreAsync(parameters, true);
            UserStores = await _storeAdminClient.GetUserStoresAsync();
        }
        else if (User.IsInRole("Store"))
        {
            var user = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
            var userStores = await _storeAdminClient.GetUserStoresAsync();
            var userStore = userStores.FirstOrDefault(us => us.UserId == user.Id);

            if (userStore != null)
            {
                UserStores.Add(userStore);
                parameters.StoreId = userStore.Id;
                result = await _storeAdminClient.GetSalesForStoreAsync(parameters, false);
            }
        }

        if (result != null)
        {
            result.StoresSelect = UserStores.ToArray();
        }

        return new JsonResult(result);
    }

    public async Task<IActionResult> OnPostSearch()
    {
        parameters.TakePage = 100;
        if (User.IsInRole("SupperUser"))
        {
            result = await _storeAdminClient.GetSalesForStoreAsync(parameters, true);
            UserStores = await _storeAdminClient.GetUserStoresAsync();
        }
        else if (User.IsInRole("Store"))
        {
            var user = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
            var userStores = await _storeAdminClient.GetUserStoresAsync();
            var userStore = userStores.FirstOrDefault(us => us.UserId == user.Id);

            if (userStore != null)
            {
                UserStores.Add(userStore);
                parameters.StoreId = userStore.Id;
                result = await _storeAdminClient.GetSalesForStoreAsync(parameters, false);
            }
        }

        return new JsonResult(result);
    }

    #region Get Statuses

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostGetStatuses(int OrderId)
    {
        var list = await _storeAdminClient.GetOrderStatusesAsync(OrderId);
        Response.Object = list;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Contract

    public async Task<IActionResult> OnPostGetContract(int OrderId)
    {
        if (User.IsInRole("SupperUser") || User.IsInRole("Sale") || User.IsInRole("Finanicial"))
        {
            var res = await _storeAdminClient.GetContractForOrderAsync(OrderId, "All");
            return new JsonResult(res);
        }
        else if (User.IsInRole("Store"))
        {
            var user = await _userClient.GetUserByUsernameAsync(User.Identity.Name);
            var userStores = await _storeAdminClient.GetUserStoresAsync();
            var userStore = userStores.FirstOrDefault(us => us.UserId == user.Id);

            if (userStore != null)
            {
                var res = await _storeAdminClient.GetContractForOrderAsync(OrderId, userStore.LatinName);
                return new JsonResult(res);
            }
        }

        return new JsonResult(Response);
    }

    #endregion
}