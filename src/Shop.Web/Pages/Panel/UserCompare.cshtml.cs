using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.Features.UserProduct.Requests.Query;

namespace Shop.Web.Pages.Panel
{
    [Authorize]
    public class UserCompareModel : PageModel
    {
        #region Constractor
        private readonly IMediator _mediator;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserRepository _userRep;
        public UserCompareModel(IMediator mediator,
            SignInManager<IdentityUser> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;
        }
        #endregion
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostData()
        {
            var result = await _mediator.Send(new GetCmparisonProductsOfUsersQueryReq(User.Identity.Name));
            return new JsonResult(result);
        }
    }
}
