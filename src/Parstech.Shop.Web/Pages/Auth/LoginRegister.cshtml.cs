using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.Web.Pages.Auth;

public class LoginRegisterModel : PageModel
{
    private readonly IAuthAdminGrpcClient _authClient;

    private readonly SignInManager<IdentityUser> _signInManager;

    public LoginRegisterModel(IAuthAdminGrpcClient authClient, SignInManager<IdentityUser> signInManager)
    {
        _authClient = authClient;
        _signInManager = signInManager;
    }

    public ResponseDto Response { get; set; }


    public void OnGet(string backUrl)
    {
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostLoginRequest(string loginmobile)
    {
        var result = await _authClient.LoginOrRegisterRequestAsync(loginmobile);
        Response = result;
        return new(Response);
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostLogin(string loginmobile, string loginactiveCode, string ActiveRegister)
    {
        if (ActiveRegister == null)
        {
            var result = await _authClient.LoginByActiveCodeAsync(loginmobile, loginactiveCode);
            Response = result;
            return new(Response);
        }
        else
        {
            if (loginactiveCode == ActiveRegister)
            {
                var result = await _authClient.LoginByActiveCodeAsync(loginmobile, loginactiveCode);
                Response = result;
                return new(Response);
            }
            else
            {
                Response = new();
                Response.IsSuccessed = false;
                Response.Message = "کد تایید نادرست است";
                return new(Response);
            }
        }
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostPasswordLogin(string loginmobile, string loginpassword)
    {
        var result = await _authClient.LoginByPasswordAsync(loginmobile, loginpassword);
        Response = result;
        return new(Response);
    }
}