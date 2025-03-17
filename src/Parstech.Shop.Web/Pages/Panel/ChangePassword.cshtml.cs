using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class ChangePasswordModel : PageModel
{
    #region Constructor

    private readonly UserPreferencesGrpcClient _userPreferencesClient;

    public ChangePasswordModel(UserPreferencesGrpcClient userPreferencesClient)
    {
        _userPreferencesClient = userPreferencesClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public ChangePasswordDto Input { get; set; }

    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    #endregion

    #region Get

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        #region Validator

        var validator = new ChangePasswordDtoValidator();
        var valid = validator.Validate(Input);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Object = Input;
            return new JsonResult(Response);
        }

        #endregion

        var result = await _userPreferencesClient.ChangePasswordAsync(
            Input.old,
            Input.newPassword,
            Input.confirmPassword);

        Response.IsSuccessed = result.IsSuccess;
        Response.Message = result.Message;

        if (!result.IsSuccess && result.ErrorMessages.Count > 0)
        {
            Response.Errors = result.ErrorMessages.Select(e => new FluentValidation.Results.ValidationFailure("", e))
                .ToList();
        }

        return new JsonResult(Response);
    }

    #endregion
}