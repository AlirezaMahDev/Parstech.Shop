// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Parstech.Shop.Web.Services.GrpcClients;
using Shop.Application.DTOs.Auth;
using Shop.Application.DTOs.Response;
using Shop.Application.Validators.Auth;

namespace Parstech.Shop.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserAuthGrpcClient _userAuthClient;

        public LoginModel(
            SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            UserAuthGrpcClient userAuthClient)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userAuthClient = userAuthClient;
        }

        [BindProperty]
        public LoginDto Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        [BindProperty] 
        public ResponseDto Response { get; set; } = new ResponseDto();

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            #region Validator
            var validator = new LoginDtoValidator();
            var valid = validator.Validate(Input);
            if (!valid.IsValid)
            {
                Response.IsSuccessed = false;
                Response.Errors = valid.Errors;
                Response.Object = Input;
                return new JsonResult(Response);
            }
            #endregion

            var loginResponse = await _userAuthClient.LoginAsync(Input.UserName, Input.Password);

            Response.IsSuccessed = loginResponse.IsSuccessful;
            Response.Message = loginResponse.Message;
            
            if (loginResponse.IsSuccessful)
            {
                Response.Object = loginResponse.RedirectUrl;
                Response.Object2 = loginResponse.ProtectedData;
            }
            
            return new JsonResult(Response);
        }
    }
}
