using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.Pages.Admin.Setting;

[Authorize(Roles = "SupperUser")]
public class SiteModel : PageModel
{
    private readonly ISettingsAdminGrpcClient _settingsClient;

    public SiteModel(ISettingsAdminGrpcClient settingsClient)
    {
        _settingsClient = settingsClient;
    }

    [BindProperty]
    public SiteDto Input { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new ResponseDto();

    public async Task<IActionResult> OnGet()
    {
        Input = await _settingsClient.GetSiteSettingsAsync(1);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        #region Validator

        var validator = new SiteDtoValidator();
        var valid = validator.Validate(Input);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Object = Input;
            return new JsonResult(Response);
        }

        #endregion

        Response = await _settingsClient.UpdateSiteSettingsAsync(1, Input);
        return new JsonResult(Response);
    }
}