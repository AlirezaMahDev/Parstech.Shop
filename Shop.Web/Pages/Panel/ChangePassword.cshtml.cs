using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.DTOs.Auth;
using Shop.Application.DTOs.Response;
using Shop.Application.Validators.Auth;

namespace Shop.Web.Pages.Panel
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        #region Constractor

        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ChangePasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMediator mediator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mediator = mediator;
        }

        #endregion
        #region Properties

        [BindProperty]
        public ChangePasswordDto Input { get; set; }
        [BindProperty]
        public ResponseDto Response { get; set; } = new ResponseDto();


        #endregion
        #region Get
        public async Task<IActionResult> OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            #region Validator
            var validator = new ChangePasswordDtoValidator();
            var valid = validator.Validate(Input);
            if (!valid.IsValid)
            {
                Response.IsSuccessed = false;
                Response.Errors = valid.Errors;
                Response.Object = Input;
                return new JsonResult(Response);
            }
            #endregion


            var user = await _userManager.GetUserAsync(User);

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.old, Input.newPassword);
            if (!changePasswordResult.Succeeded)
            {

                Response.IsSuccessed = false;
                Response.Message = "اطلاعات به درستی وراد نشده است";
                return new JsonResult(Response);
            }

            await _signInManager.RefreshSignInAsync(user);
            Response.IsSuccessed = false;
            Response.Message = "عملیات احراز هویت با موفقیت تفییر یافت";
            return new JsonResult(Response);
        }


        #endregion


    }



}
