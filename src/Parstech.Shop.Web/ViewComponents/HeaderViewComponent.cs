using Microsoft.AspNetCore.Mvc;

using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.ViewComponents;

[ViewComponent(Name = "Header")]
public class HeaderViewComponent : ViewComponent
{
    private readonly SiteSettingGrpcClient _siteSettingGrpcClient;

    public HeaderViewComponent(SiteSettingGrpcClient siteSettingGrpcClient)
    {
        _siteSettingGrpcClient = siteSettingGrpcClient;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var settings = await _siteSettingGrpcClient.GetSettingAndSeoAsync();
        return View(settings);
    }
}