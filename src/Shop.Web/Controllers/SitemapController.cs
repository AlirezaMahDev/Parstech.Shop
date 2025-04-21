using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.DTOs.SiteMap;
using Shop.Application.Features.Api.Requests.Queries;

namespace Shop.Web.Controllers
{
    [Route("sitemap.xml")]
    [ApiController]
    public class SitemapController : ControllerBase
    {

        private readonly IMediator _mediator;
        public SitemapController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}";

            var sitemapNodes = new List<SitemapDto>();

            SitemapDto landing = new SitemapDto()
            {
                loc = baseUrl,
                lastmod = DateTime.Now.ToString(),
                priority = "1.00"
            };
            sitemapNodes.Add(landing);

            SitemapDto AboutUs = new SitemapDto()
            {
                loc = $"{baseUrl}/AboutUs",
                lastmod = DateTime.Now.ToString(),
                priority = "0.70"
            };
            sitemapNodes.Add(AboutUs);

            SitemapDto ContactUs = new SitemapDto()
            {
                loc = $"{baseUrl}/ContactUs",
                lastmod = DateTime.Now.ToString(),
                priority = "0.70"
            };
            sitemapNodes.Add(ContactUs);


            var All = await _mediator.Send(new SiteMapGenerateQueryReq());
            foreach (var item in All)
            {
                item.loc = $"{baseUrl}{item.loc}";
                sitemapNodes.Add(item);
            }




            var sitemap = string.Join("\n", sitemapNodes.Select(url => $"<url><loc>{url.loc}</loc><lastmod>{url.lastmod}</lastmod><priority>{url.priority}</priority ></url>"));
            var xml = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">
                            {sitemap}
                        </urlset>";

            return Content(xml, "application/xml");
        }
    }
}
