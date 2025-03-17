using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin;

[Authorize(Roles = "SupperUser,Inventory,Sale,Finanicial,Store")]
public class IndexModel : PageModel
{
    #region Constructor

    private readonly IDashboardAdminGrpcClient _dashboardClient;

    public IndexModel(IDashboardAdminGrpcClient dashboardClient)
    {
        _dashboardClient = dashboardClient;
    }

    #endregion

    #region Property

    public IndexCountsDto IndexReport { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    #endregion

    #region Get

    public void OnGet()
    {
    }

    #endregion

    #region Data

    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> OnPostGetData()
    {
        IndexReport = await _dashboardClient.GetDashboardCountsAsync();
        Response.IsSuccessed = true;
        Response.Object = IndexReport;
        return new JsonResult(Response);
    }

    #endregion
}