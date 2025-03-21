﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Web.Services;

namespace Parstech.Shop.Web.Pages.Auth;

public class RegisterModel : PageModel

{
    private readonly ISixLaborsCaptchaModule sixLaborsCaptcha;
    private readonly IDistributedCache distributedCache;
    private readonly IAuthAdminGrpcClient _authClient;

    public RegisterModel(ISixLaborsCaptchaModule sixLaborsCaptcha,
        IDistributedCache distributedCache,
        IAuthAdminGrpcClient authClient)
    {
        this.sixLaborsCaptcha = sixLaborsCaptcha;
        this.distributedCache = distributedCache;
        _authClient = authClient;
    }

    public record CaptchaResponse(string Id, string Image);

    public ResponseDto Response { get; set; } = new();

    [BindProperty]
    public RegisterDto Inputs { get; set; }

    public void OnGet()
    {
    }

    [ValidateAntiForgeryToken]
    public async Task<JsonResult> OnPostRegister()
    {
        string? captchaValue = await distributedCache.GetStringAsync(Inputs.CaptchaKey);
        //await distributedCache.RemoveAsync(request.CaptchaKey, cancellationToken);

        if (string.IsNullOrWhiteSpace(captchaValue))
        {
            Response.IsSuccessed = false;
            Response.Message = "کد امنیتی منقضی شده است";
            return new(Response);
        }

        if (captchaValue != Inputs.CaptchaValue)
        {
            Response.IsSuccessed = false;
            Response.Message = "کد امنیتی نادرست است";
            return new(Response);
        }

        #region Validator

        var validator = new RegisterDtoValidator();
        var valid = validator.Validate(Inputs);
        if (!valid.IsValid)
        {
            Response.IsSuccessed = false;
            Response.Errors = valid.Errors;
            Response.Message = valid.Errors.FirstOrDefault().ErrorMessage;

            return new(Response);
        }

        #endregion


        UserRegisterDto input = new();
        input.UserName = Inputs.Mobile;
        input.FirstName = Inputs.Name;
        input.LastName = Inputs.Family;
        input.NationalCode = Inputs.MeliCode;
        input.Country = "ایران";
        input.State = Inputs.State;
        input.City = Inputs.City;
        input.Address = Inputs.Address;
        input.Mobile = Inputs.Mobile;

        var result = await _authClient.RegisterUserAsync(input);

        return new(result);
    }

    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<JsonResult> OnPostCaptcha()
    {
        var value =
            SixLaborsCaptcha.Core.Extensions.GetUniqueKey(4, "0123456789".ToCharArray());
        string key = Guid.NewGuid().ToString();
        var captcha = sixLaborsCaptcha.Generate(value);
        await distributedCache.SetStringAsync(key,
            value,
            new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) });
        CaptchaResponse res = new(key, Convert.ToBase64String(captcha));
        return new(res);
    }
}