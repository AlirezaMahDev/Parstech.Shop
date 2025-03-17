using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Parstech.Shop.Web.Pages;

public class AboutUs : PageModel
{
    private readonly ILogger<AboutUs> _logger;

    public AboutUs(ILogger<AboutUs> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}