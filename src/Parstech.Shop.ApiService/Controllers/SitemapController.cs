using Microsoft.AspNetCore.Mvc;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Controllers;

[Route("sitemap.xml")]
[ApiController]
public class SitemapController : ControllerBase
{
    private readonly CategoryGrpcClient _categoryClient;
    private readonly ProductGrpcClient _productClient;

    public SitemapController(
        CategoryGrpcClient categoryClient,
        ProductGrpcClient productClient)
    {
        _categoryClient = categoryClient;
        _productClient = productClient;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        string baseUrl = $"{Request.Scheme}://{Request.Host.ToUriComponent()}";

        var sitemapNodes = new List<SitemapDto>();

        // Add static pages
        sitemapNodes.Add(new() { loc = baseUrl, lastmod = DateTime.Now.ToString(), priority = "1.00" });

        sitemapNodes.Add(new()
        {
            loc = $"{baseUrl}/AboutUs", lastmod = DateTime.Now.ToString(), priority = "0.70"
        });

        sitemapNodes.Add(new()
        {
            loc = $"{baseUrl}/ContactUs", lastmod = DateTime.Now.ToString(), priority = "0.70"
        });

        // Get categories for sitemap
        var categories = await _categoryClient.GetParentCategoriesAsync();
        foreach (var category in categories)
        {
            sitemapNodes.Add(new()
            {
                loc = $"{baseUrl}/Products/Category/{category.LatinName}",
                lastmod = DateTime.Now.ToString(),
                priority = "0.80"
            });

            var subCategories = await _categoryClient.GetSubCategoriesAsync(category.Id);
            foreach (var subCategory in subCategories)
            {
                sitemapNodes.Add(new()
                {
                    loc = $"{baseUrl}/Products/Category/{subCategory.LatinName}",
                    lastmod = DateTime.Now.ToString(),
                    priority = "0.80"
                });
            }
        }

        // Get products for sitemap
        var products = await _productClient.GetProductsForSitemapAsync();
        foreach (var product in products)
        {
            sitemapNodes.Add(new()
            {
                loc = $"{baseUrl}/Products/Detail/{product.ShortLink}/{product.Id}",
                lastmod = product.UpdatedAt ?? DateTime.Now.ToString(),
                priority = "0.90"
            });
        }

        string sitemap = string.Join("\n",
            sitemapNodes.Select(url =>
                $"<url><loc>{url.loc}</loc><lastmod>{url.lastmod}</lastmod><priority>{url.priority}</priority></url>"));
        string xml = $@"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">
                            {sitemap}
                        </urlset>";

        return Content(xml, "application/xml");
    }
}