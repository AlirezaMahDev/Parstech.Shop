using Microsoft.AspNetCore.Mvc;

using Parstech.Shop.Web.Services.GrpcClients;

namespace Parstech.Shop.Web.ViewComponents;

[ViewComponent(Name = "UserName")]
public class UserNameViewComponent : ViewComponent
{
    private readonly UserProfileGrpcClient _userProfileClient;

    public UserNameViewComponent(UserProfileGrpcClient userProfileClient)
    {
        _userProfileClient = userProfileClient;
    }

    public async Task<IViewComponentResult> InvokeAsync(string userName, string position)
    {
        var request = new UserInfoRequest { Username = userName, Position = position };
        var userInfo = await _userProfileClient.GetUserInfoAsync(request);
        return View(userInfo);
    }
}