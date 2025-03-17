using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class UserCompareModel : PageModel
{
    #region Constructor

    private readonly UserPreferencesGrpcClient _userPreferencesClient;

    public UserCompareModel(UserPreferencesGrpcClient userPreferencesClient)
    {
        _userPreferencesClient = userPreferencesClient;
    }

    #endregion

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostData()
    {
        var result = await _userPreferencesClient.GetComparisonProductsAsync(User.Identity.Name);
        return new JsonResult(result.Products);
    }
}