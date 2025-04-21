using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SixLaborsCaptcha.Core;
using static Shop.Web.Pages.Auth.RegisterModel;

using Extensions = SixLaborsCaptcha.Core.Extensions;

namespace Shop.Web.Controllers
{
    [Route("CaptchaRequest")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ISixLaborsCaptchaModule sixLaborsCaptcha;
        private readonly IDistributedCache distributedCache;
        
        public CaptchaController(ISixLaborsCaptchaModule sixLaborsCaptcha, IDistributedCache distributedCache)
        {
            this.sixLaborsCaptcha = sixLaborsCaptcha;
            this.distributedCache = distributedCache;
           
        }
        public async Task<IActionResult>  Index()
        {
            var value =Extensions.GetUniqueKey(4, "0123456789".ToCharArray());
            var key = Guid.NewGuid().ToString();
            var captcha = sixLaborsCaptcha.Generate(value);
            await distributedCache.SetStringAsync(key,
                value,
                new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(5) });
            var res = new CaptchaResponse(key, Convert.ToBase64String(captcha));
            return new JsonResult(res);
        }
    }
}
