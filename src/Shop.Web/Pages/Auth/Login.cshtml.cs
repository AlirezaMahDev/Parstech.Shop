// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.Auth;
using Shop.Application.DTOs.Response;
using Shop.Application.Features.Security.Requests.Queries;
using Shop.Application.Validators.Auth;

namespace Shop.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IUserRepository _userRep;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<IdentityUser> signInManager,
            ILogger<LoginModel> logger,
            IMediator mediator,
            IUserRepository userRep,
            IMapper mapper)
        {
            _signInManager = signInManager;
            _logger = logger;
            _mediator = mediator;
            _userRep = userRep;
            _mapper = mapper;
        }


        [BindProperty]
        public LoginDto Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        public string ReturnUrl { get; set; }


        [TempData]
        public string ErrorMessage { get; set; }


        [BindProperty] public ResponseDto Response { get; set; } = new ResponseDto();


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



            var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, true, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                //var user=await _userRep.GetUserByUserName(Input.UserName);
                //user.LastLoginDate = DateTime.Now;
                //await _mediator.Send(new UserUpdateCommandReq(_mapper.Map<UserDto>(user)));
                Response.IsSuccessed = true;
                Response.Message = "ورود با موفقیت انجام شد . در حال انتقال به پنل";
                if (User.IsInRole("Customer"))
                {
                    Response.Object = "/Panel";
                }
                else
                {
                    Response.Object = "/Admin";
                }
                Response.Object2 = await _mediator.Send(new DataProtectQueryReq(Input.UserName, "protect"));
            }
            else
            {
                Response.IsSuccessed = false;
                Response.Message = "کاربری با مشخصات وارد شده یافت نشد";
            }
            if (result.IsLockedOut)
            {
                Response.IsSuccessed = false;
                Response.Message = "حساب شما تا تاریخ فلان مسدود شده است.";

            }

            return new JsonResult(Response);
        }
    }
}
