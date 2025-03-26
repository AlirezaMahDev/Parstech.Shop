using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.FormCredit;
using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.Features.FormCredit.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Requests;

public class CreditRequestsModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;

    public CreditRequestsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion

    #region Properties

    //paging parameter
    [BindProperty]
    public ParameterDto Parameter { get; set; } = new();



    //products
    [BindProperty]
    public List<FormCreditDto> List { get; set; }



    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {

        return Page();
    }

    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OnPostData(int skip)
    {
        //Parameter.CurrentPage = 1;

        Parameter.Skip = skip;
        List = await _mediator.Send(new PagingFormCreditsQueryReq(Parameter));

        return new JsonResult(List);
    }


    public async Task<IActionResult> OnPostChangeStatus(int Id, string Type)
    {
        var result = await _mediator.Send(new ChangeStatusFormCreditQueryReq(Id, Type));
        return new JsonResult(result);
    }
    #endregion
}