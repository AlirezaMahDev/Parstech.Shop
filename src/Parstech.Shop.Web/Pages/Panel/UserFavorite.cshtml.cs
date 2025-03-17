using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class UserFavoriteModel : PageModel
{
    #region Constructor

    private readonly UserPreferencesGrpcClient _userPreferencesClient;

    public UserFavoriteModel(UserPreferencesGrpcClient userPreferencesClient)
    {
        _userPreferencesClient = userPreferencesClient;
    }

    #endregion

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostData()
    {
        var result = await _userPreferencesClient.GetFavoriteProductsAsync(User.Identity.Name);
        return new JsonResult(result.Products);
    }
}