using MediatR;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.Features.Security.Requests.Queries;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.Web.Pages.Auth;

public class LoginRegisterModel : PageModel
{
    private readonly IMediator _mediator;

    private readonly SignInManager<IdentityUser> _signInManager;
    public LoginRegisterModel(IMediator mediator, SignInManager<IdentityUser> signInManager)
    {
        _mediator = mediator;
        _signInManager = signInManager;
    }

    public ResponseDto Response { get; set; }



    public void OnGet(string backUrl)
    {
    }
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostLoginRequest(string loginmobile)
    {

        var result = await _mediator.Send(new LoginOrRegisterRequestQueryReq(loginmobile));
        Response = result;
        return new(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostLogin(string loginmobile, string loginactiveCode, string ActiveRegister)
    {
        if (ActiveRegister == null)
        {
            var result = await _mediator.Send(new LoginByActiveCodeQueryReq(loginmobile, loginactiveCode));
            Response = result;
            return new(Response);
        }
        else
        {
            if (loginactiveCode == ActiveRegister)
            {
                UserRegisterDto input = new();
                input.UserName = loginmobile;
                input.FirstName = " ";
                input.LastName = " ";
                input.NationalCode = " ";
                input.Country = "ایران";
                input.State = "تهران";
                input.City = "تهران";
                input.Address = "-";
                input.Mobile = loginmobile;
                input.ActiveCode = loginactiveCode;


                var result = await _mediator.Send(new UserRegisterQueryReq(input));
                var result2 = await _mediator.Send(new LoginByActiveCodeQueryReq(loginmobile, loginactiveCode));
                Response = result2;
                return new(Response);

            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "کد تائید وارد شده نادرست است";
                return new(Response);
            }



        }

    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostPasswordLogin(string loginmobile, string loginpassword)
    {
        var result = await _signInManager.PasswordSignInAsync(loginmobile, loginpassword, true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            //var user=await _userRep.GetUserByUserName(Input.UserName);
            //user.LastLoginDate = DateTime.Now;
            //await _mediator.Send(new UserUpdateCommandReq(_mapper.Map<UserDto>(user)));
            Response.IsSuccessed = true;
            Response.Message = "ورود با موفقیت انجام شد . در حال انتقال به پنل";
            if (User.IsInRole("Customer"))
            {
                Response.Object = "/Panel";
            }
            else
            {
                Response.Object = "/Admin";
            }
            Response.Object2 = await _mediator.Send(new DataProtectQueryReq(loginmobile, "protect"));
        }
        else
        {
            Response.IsSuccessed = false;
            Response.Message = "کاربری با مشخصات وارد شده یافت نشد";
        }
        if (result.IsLockedOut)
        {
            Response.IsSuccessed = false;
            Response.Message = "حساب شما تا تاریخ فلان مسدود شده است.";

        }




        return new(Response);
    }
}