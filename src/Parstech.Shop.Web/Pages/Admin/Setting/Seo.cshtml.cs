using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Admin.Setting;

[Authorize(Roles = "SupperUser")]
public class SeoModel : PageModel
{
    private readonly ISettingsAdminGrpcClient _settingsClient;

    public SeoModel(ISettingsAdminGrpcClient settingsClient)
    {
        _settingsClient = settingsClient;
    }

    [BindProperty]
    public SeoDto Input { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    public async Task<IActionResult> OnGet()
    {
        Input = await _settingsClient.GetSeoSettingsAsync(1);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        #region Validator

        var validator = new SeoDtoValidator();
        var valid = validator.Validate(Input);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Object = Input;
            return new JsonResult(Response);
        }

        #endregion

        Response = await _settingsClient.UpdateSeoSettingsAsync(1, Input);
        return new JsonResult(Response);
    }
}