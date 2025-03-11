using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Response;
using Shop.Application.DTOs.SocialSetting;
using Shop.Application.Features.SocialSetting.Requests.Commands;
using Shop.Application.Validators.SocialSetting;

namespace Shop.Web.Pages.Admin.Setting
{
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


        [BindProperty] public ResponseDto Response { get; set; } = new ResponseDto();

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
}
