using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.SocialSetting;
using Parstech.Shop.Context.Application.Features.SocialSetting.Requests.Commands;
using Parstech.Shop.Context.Application.Validators.SocialSetting;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Setting;

[Authorize(Roles = "SupperUser")]
public class SocialModel : PageModel
{
    private readonly IMediator _mediator;

    public SocialModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    [BindProperty]
    public List<SocialSettingDto> List { get; set; }

    [BindProperty]
    public SocialSettingDto Input { get; set; }


    [BindProperty]
    public int Id { get; set; }


    [BindProperty] public ResponseDto Response { get; set; } = new();

    public async Task<IActionResult> OnGet()
    {
        List = await _mediator.Send(new SocialSettingListReadCommandReq());
        return Page();
    }
    public async Task<IActionResult> OnPostGetOne()
    {
        Input = await _mediator.Send(new SocialSettingReadCommandReq(Id));
        Response.Object = Input;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPost()
    {

        #region Validator
        var validator = new SocialSettingDtoValidator();
        var valid = validator.Validate(Input);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Object = Input;
            return new JsonResult(Response);
        }
        #endregion

        if (Input.Id != 0)
        {
            await _mediator.Send(new SocialSettingUpdateCommandReq(Input));
            Response.IsSuccessed = true;
            Response.Message = "شبکه اجتماعی با موفقیت ویرایش شد";
        }
        else
        {
            await _mediator.Send(new SocialSettingCreateCommandReq(Input));
            Response.IsSuccessed = true;
            Response.Message = "شبکه اجتماعی با موفقیت افزوده شد";
        }

        //Response.Object = Input;

        return new JsonResult(Response);
    }


}