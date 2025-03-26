using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.IUserRole;
using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.DTOs.UserBilling;
using Parstech.Shop.Context.Application.DTOs.UserShipping;
using Parstech.Shop.Context.Application.Features.User.Requests.Commands;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserBilling.Requests.Commands;
using Parstech.Shop.Context.Application.Features.UserBilling.Requests.Queries;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Commands;
using Parstech.Shop.Context.Application.Features.UserShipping.Requests.Queries;

namespace Parstech.Shop.Web.Admin.Pages.Admin.Users;

[Authorize(Roles = "SupperUser,Inventory,Finanicial,Sale")]
public class IndexModel : PageModel
{
    #region Constractor

    private readonly IMediator _mediator;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;


    public IndexModel(IMediator mediator,
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;

        _userManager = userManager;
    }

    #endregion

    #region Properties

    [BindProperty]
    public UserParameterDto Parameter { get; set; } = new();

    [BindProperty]
    public UserPageingDto List { get; set; }


    [BindProperty]
    public ResponseDto Response { get; set; } = new();


    [BindProperty]
    public UserBillingDto UserBillingDto { get; set; }

    [BindProperty]
    public List<UserShippingDto> UserShippingList { get; set; }

    [BindProperty]
    public UserShippingDto UserShippingDto { get; set; }


    [BindProperty]
    public List<IUserRoleDto> UserRoleList { get; set; }

    [BindProperty]
    public IUserRoleDto UserRole { get; set; }



    [BindProperty]
    public int IUserId { get; set; }

    [BindProperty]
    public string SUserId { get; set; }

    [BindProperty]
    public int ShippingId { get; set; }

    [BindProperty]
    public UserRegisterDto UserRegisterDto { get; set; }

    [BindProperty]
    public List<UserFilterDto> UserFilterDtos { get; set; }

    #endregion

    #region Get

    public async Task<IActionResult> OnGet()
    {
        UserFilterDtos = await _mediator.Send(new UserFilterDataQueryReq());
        return Page();
    }

    public async Task<IActionResult> OnPostData()
    {

        Parameter.TakePage = 30;
        List = await _mediator.Send(new UserPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion

    #region Search Paging

    public async Task<IActionResult> OnPostSearch()
    {
        Parameter.CurrentPage = 1;
        Parameter.TakePage = 30;
        List = await _mediator.Send(new UserPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostPaging()
    {
        Parameter.TakePage = 30;
        List = await _mediator.Send(new UserPagingQueryReq(Parameter));
        Response.Object = List;
        Response.IsSuccessed = true;
        return new JsonResult(Response);
    }

    #endregion


    #region Billing

    public async Task<IActionResult> OnPostBilling()
    {
        UserBillingDto = await _mediator.Send(new UserBillingOfUserQueryReq(IUserId));
        Response.Object = UserBillingDto;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreateBilling()
    {
        if (UserBillingDto.Id != null)
        {
            await _mediator.Send(new UserBillingUpdateCommandReq(UserBillingDto));
            Response.Object = UserBillingDto;
            Response.IsSuccessed = true;
            Response.Message = "اطلاعات حقوقی کاربر با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        else
        {
            await _mediator.Send(new UserBillingCreateCommandReq(UserBillingDto));
            Response.Object = UserBillingDto;
            Response.IsSuccessed = true;
            Response.Message = "اطلاعات حقوقی کاربر با موفقیت ثبت شد";
            return new JsonResult(Response);
        }

    }

    #endregion


    #region Shipping

    public async Task<IActionResult> OnPostShippingList()
    {
        UserShippingList = await _mediator.Send(new UserShippingOfUserQueryReq(IUserId));
        Response.Object = UserShippingList;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostShipping()
    {
        UserShippingDto = await _mediator.Send(new UserShippingReadCommandReq(ShippingId));
        Response.Object = UserShippingDto;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostEditOrCreateShipping()
    {

        if (UserShippingDto.Id != null)
        {
            await _mediator.Send(new UserShippingUpdateCommandReq(UserShippingDto));
            Response.Object = UserShippingDto;
            Response.IsSuccessed = true;
            Response.Message = "آدرس کاربر با موفقیت ویرایش شد";
            return new JsonResult(Response);
        }
        else
        {
            await _mediator.Send(new UserShippingCreateCommandReq(UserShippingDto));
            Response.Object = UserShippingDto;
            Response.IsSuccessed = true;
            Response.Message = "آدرس کاربر با موفقیت ثبت شد";
            return new JsonResult(Response);
        }
    }
    public async Task<IActionResult> OnPostDeleteShipping()
    {
        await _mediator.Send(new UserShippingDeleteCommandReq(ShippingId));
        Response.Object = UserShippingDto;
        Response.IsSuccessed = true;
        Response.Message = " آدرس کاربر با موفقیت حذف شد";
        return new JsonResult(Response);
    }
    #endregion


    #region Permission

    public async Task<IActionResult> OnPostRoles()
    {
        var user = await _mediator.Send(new UserReadCommandReq(IUserId));
        UserRoleList = await _mediator.Send(new UserRoleListQueryReq(user.UserId));
        Response.Object = UserRoleList;
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostAddRole()
    {
        await _mediator.Send(new UserRoleCreateQueryReq(UserRole));
        Response.IsSuccessed = true;
        Response.Message = "دسترسی کاربر با موفقیت ثبت شد";
        return new JsonResult(Response);
    }

    public async Task<IActionResult> OnPostDeleteRole()
    {
        await _mediator.Send(new UserRoleDeleteQueryReq(UserRole));
        Response.IsSuccessed = true;
        Response.Message = "دسترسی کاربر با موفقیت لغو شد";
        return new JsonResult(Response);
    }

    #endregion


    #region CreateUser

    public async Task<IActionResult> OnPostCreateUser()
    {
        await _mediator.Send(new UserRegisterQueryReq(UserRegisterDto));
        Response.IsSuccessed = true;
        Response.Message = "کاربر با موفقیت ثبت شد";
        return new JsonResult(Response);
    }

    #endregion

    #region Login By User
    public async Task<IActionResult> OnPostLoginByUser(int loginUserId)
    {
        var user = await _mediator.Send(new UserReadCommandReq(loginUserId));
        var iuser = await _userManager.FindByNameAsync(user.UserName);
        if (iuser != null)
        {
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(iuser, true);
            return Redirect("/");
        }
        return Page();

    }
    #endregion
}