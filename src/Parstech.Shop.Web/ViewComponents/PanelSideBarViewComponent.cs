using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.ViewComponents;

[ViewComponent(Name = "PanelSideBar")]
public class PanelSideBarViewComponent : ViewComponent
{
    private readonly UserProfileGrpcClient _userProfileClient;
    private readonly WalletGrpcClient _walletClient;

    public PanelSideBarViewComponent(UserProfileGrpcClient userProfileClient, WalletGrpcClient walletClient)
    {
        _userProfileClient = userProfileClient;
        _walletClient = walletClient;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var username = User.FindFirstValue(ClaimTypes.Name);
        var user = await _userProfileClient.GetUserByUsernameAsync(username);
        var wallet = await _walletClient.GetWalletByUserIdAsync(user.Id);

        ViewBag.User = user;
        ViewBag.Wallet = wallet;

        return View();
    }
}