using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.FormCredit;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.Features.FormCredit.Requests.Commands;
using Parstech.Shop.Context.Application.Validators.FormCredit;

namespace Parstech.Shop.Web.Pages;

public class CreditRequest : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;

    public CreditRequest(IMediator mediator)
    {
        _mediator = mediator;
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
        formCredit.RequestPrice = long.Parse(formCredit.TextRequestPrice.Replace(",", ""));
        Response = await _mediator.Send(new CreateFormCreditCommandReq(formCredit));
        return new JsonResult(Response);
    }
    #endregion
}