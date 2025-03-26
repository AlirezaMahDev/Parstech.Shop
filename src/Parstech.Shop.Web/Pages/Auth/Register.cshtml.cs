using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Distributed;

using Parstech.Shop.Context.Application.DTOs.Response;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;
using Parstech.Shop.Context.Application.Validators.User;

using SixLaborsCaptcha.Core;

namespace Parstech.Shop.Web.Pages.Auth;

public class RegisterModel : PageModel
        
{
    private readonly ISixLaborsCaptchaModule sixLaborsCaptcha;
    private readonly IDistributedCache distributedCache;
    private readonly IMediator _mediator;
        
    public RegisterModel(ISixLaborsCaptchaModule sixLaborsCaptcha, IDistributedCache distributedCache, IMediator mediator)
    {
        this.sixLaborsCaptcha = sixLaborsCaptcha;
        this.distributedCache = distributedCache;
        _mediator = mediator;
            
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

        var captchaValue = await distributedCache.GetStringAsync(Inputs.CaptchaKey);
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

        var result = await _mediator.Send(new UserRegisterQueryReq(input));

        return new(result);
    }

    [ValidateAntiForgeryToken]
    [AllowAnonymous]
    public async Task<JsonResult> OnPostCaptcha()
    {
        var value =
            Extensions.GetUniqueKey(4, "0123456789".ToCharArray());
        var key = Guid.NewGuid().ToString();
        var captcha = sixLaborsCaptcha.Generate(value);
        await distributedCache.SetStringAsync(key,
            value,
            new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) });
        var res= new CaptchaResponse(key, Convert.ToBase64String(captcha));
        return new(res);
    }
}