using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.UserBilling;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserBilling.Requests.Commands;
using Parstech.Shop.Context.Application.Features.UserBilling.Requests.Queries;
using Parstech.Shop.Context.Application.Features.Wallet.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Panel;

[Authorize]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    public IndexModel(IMediator mediator)
    {
        _mediator = mediator;
    }

    #endregion
    #region Properties


    [BindProperty]
    public ResponseDto Response { get; set; } = new();

    [BindProperty]
    public int UserId { get; set; }

    [BindProperty]
    public UserBillingDto UserBillingDto { get; set; }

    #endregion
    #region Get
    public async Task<IActionResult> OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        UserBillingDto = await _mediator.Send(new UserBillingOfUserQueryReq(CurrentUser.Id));
        Response.IsSuccessed = true;
        Response.Object = UserBillingDto;
        return new JsonResult(Response);
    }


    #endregion
    #region Create Update

    public async Task<IActionResult> OnPostUpdate()
    {

        var updatedUserBilling = await _mediator.Send(new UserBillingUpdateCommandReq(UserBillingDto));
        Response.Object = updatedUserBilling;
        Response.IsSuccessed = true;
        Response.Message = "اطلاعات حساب کاربری با موفقیت ویرایش شد";
        return new JsonResult(Response);

    }

    #endregion

    #region Get Wallet Amount
    public async Task<IActionResult> OnPostAmount()
    {
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        var Wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(CurrentUser.Id));
        Response.Object = Wallet.Amount;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostCoin()
    {
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        var Wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(CurrentUser.Id));
        Response.Object = Wallet.Coin;
        return new JsonResult(Response);
    }
    public async Task<IActionResult> OnPostFecilities()
    {
        var CurrentUser = await _mediator.Send(new UserReadByUserNameQueryReq(User.Identity.Name));
        var Wallet = await _mediator.Send(new GetWalletByUserIdQueryReq(CurrentUser.Id));
        Response.Object = Wallet.Fecilities;
        return new JsonResult(Response);
    }
    #endregion
}