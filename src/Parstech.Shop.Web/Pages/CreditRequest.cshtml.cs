using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages;

public class CreditRequest : PageModel
{
    #region Constructor

    private readonly FormCreditGrpcClient _formCreditClient;

    public CreditRequest(FormCreditGrpcClient formCreditClient)
    {
        _formCreditClient = formCreditClient;
    }

    #endregion

    #region Properties

    [BindProperty]
    public FormCreditDto formCredit { get; set; }

    public ResponseDto Response { get; set; } = new();

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    #endregion

    #region Post

    public async Task<IActionResult> OnPost()
    {
        #region Validator

        var validator = new FormCreditDtoValidator();
        var valid = validator.Validate(formCredit);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Message = "درخواست به درستی تکمیل نشده است";

            return new JsonResult(Response);
        }

        #endregion

        // Convert request price from text format
        formCredit.RequestPrice = long.Parse(formCredit.TextRequestPrice.Replace(",", ""));

        // Use gRPC client instead of MediatR
        Response = await _formCreditClient.CreateFormCreditAsync(formCredit);

        return new JsonResult(Response);
    }

    #endregion
}