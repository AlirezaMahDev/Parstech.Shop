using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.SiteSetting;
using Parstech.Shop.Context.Application.Features.SiteSeting.Requests.Commands;
using Parstech.Shop.Context.Application.Validators.SiteSetting;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Setting;

[Authorize(Roles = "SupperUser")]
public class SiteModel : PageModel
{

    private readonly IMediator _mediator;

    public SiteModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    [BindProperty]
    public SiteDto Input { get; set; }


    [BindProperty] public ResponseDto Response { get; set; } = new();

    public async Task<IActionResult> OnGet()
    {
        Input = await _mediator.Send(new SiteSettingReadCommandReq(1));
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


        await _mediator.Send(new SiteSettingUpdateCommandReq(1, Input));
        Response.IsSuccessed = true;
        Response.Message = "ویرایش اطلاعات با موفقیت انجام شد";
        Response.Object = Input;

        return new JsonResult(Response);
    }
}