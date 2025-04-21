using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Shop.Web.Pages.Admin.Setting
{
    [Authorize(Roles = "SupperUser")]
    public class ShippingModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
