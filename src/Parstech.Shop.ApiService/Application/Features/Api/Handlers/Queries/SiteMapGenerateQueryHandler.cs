using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.Api.Handlers.Queries;

public class SiteMapGenerateQueryHandler : IRequestHandler<SiteMapGenerateQueryReq, List<SitemapDto>>
{
    private readonly string _connectionString;

    public SiteMapGenerateQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<SitemapDto>> Handle(SiteMapGenerateQueryReq request, CancellationToken cancellationToken)
    {
        List<SitemapDto> Result = new();
        string cateuriesQuery = "select dbo.Categury.LatinGroupTitle from categury where Show=1";
        List<SiteMapCategury> categuries = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<SiteMapCategury>(cateuriesQuery).ToList());

        string ProductsQuery =
            "select dbo.Product.ShortLink,dbo.ProductStockPrice.Id,dbo.Product.CreateDate from dbo.Product INNER JOIN dbo.ProductStockPrice ON DBO.ProductStockPrice.ProductId=DBO.Product.Id where dbo.Product.IsActive=1";
        List<SiteMapProducts> produtcs = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<SiteMapProducts>(ProductsQuery).ToList());

        string StoresQuery = "SELECT dbo.UserStore.LatinStoreName FROM UserStore";
        List<SiteMapStore> stores =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<SiteMapStore>(StoresQuery).ToList());


        foreach (SiteMapCategury item in categuries)
        {
            SitemapDto sitemap = new()
            {
                loc = $"/Products/{item.LatinGroupTitle}", lastmod = DateTime.Now.ToString(), priority = "0.90"
            };
            Result.Add(sitemap);
        }

        foreach (SiteMapProducts item in produtcs)
        {
            SitemapDto sitemap = new()
            {
                loc = $"/Products/Detail/{item.ShortLink}/{item.Id}",
                lastmod = item.CreateDate.ToString(),
                priority = "0.80"
            };
            Result.Add(sitemap);
        }

        foreach (SiteMapStore item in stores)
        {
            SitemapDto sitemap = new()
            {
                loc = $"/Products/Stores/{item.LatinStoreName}",
                lastmod = DateTime.Now.ToString(),
                priority = "0.50"
            };
            Result.Add(sitemap);
        }


        return Result;
    }
}