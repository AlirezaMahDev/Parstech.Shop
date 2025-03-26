using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Paging;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.UserShipping;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Commands;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class ShippingsModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    public ShippingsModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion
    #region Properties

    [BindProperty]
    public ParameterDto Parameter { get; set; } = new ParameterDto();


    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    [BindProperty]
    public int UserId { get; set; }


    [BindProperty]
    public List<UserShippingDto> UserShippingsList { get; set; }

    [BindProperty]
    public UserShippingDto UserShippingDto { get; set; }

    [BindProperty]
    public int UserShippingId { get; set; }


    #endregion
    #region Get
    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        UserShippingsList = await _mediator.Send(new UserShippingOfUserQueryReq(CurrentUser.Id));
        Response.Object = UserShippingsList;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }


    #endregion

    #region Create Update

    public async Task<IActionResult> OnPostGetItem()
    {
        UserShippingDto = await _mediator.Send(new UserShippingReadCommandReq(UserShippingId));
        Response.Object = UserShippingDto;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }


    public async Task<IActionResult> OnPostUpdateAndCreate()
    {

        if (UserShippingDto.Id == 0)
        {
            var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
            UserShippingDto.UserId = CurrentUser.Id;
            await _mediator.Send(new UserShippingCreateCommandReq(UserShippingDto));
            Response.Object = UserShippingDto;
            Response.IsSuccessed = true;
            Response.Message = "آدرس با موفقیت ثبت شد";
            return new JsonResult(Response);
        }
        else
        {
            await _mediator.Send(new UserShippingUpdateCommandReq(UserShippingDto));
            Response.Object = UserShippingDto;
            Response.IsSuccessed = true;
            Response.Message = "آدرس با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }

    }
    public async Task<IActionResult> OnPostDeleteItem()
    {
        //UserShippingDto = await _mediator.Send(new UserShippingReadCommandReq(UserShippingId));
        await _mediator.Send(new UserShippingDeleteCommandReq(UserShippingId));
        Response.IsSuccessed = true;
        Response.Message = "آدرس شما با موفقیت حذف گردید";
        return new JsonResult(Response);
    }
    #endregion
}