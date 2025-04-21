using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Web.Pages
{
    public class ContactUs : PageModel
    {
        private readonly ILogger<ContactUs> _logger;

        public ContactUs(ILogger<ContactUs> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}