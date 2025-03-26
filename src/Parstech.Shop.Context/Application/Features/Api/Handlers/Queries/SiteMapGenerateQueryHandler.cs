using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.SiteMap;
using Parstech.Shop.Context.Application.Features.Api.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Api.Handlers.Queries;

public class SiteMapGenerateQueryHandler : IRequestHandler<SiteMapGenerateQueryReq, List<SitemapDto>>
{
    private readonly string _connectionString;
    public SiteMapGenerateQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    public async Task<List<SitemapDto>> Handle(SiteMapGenerateQueryReq request, CancellationToken cancellationToken)
    {
        List<SitemapDto> Result=new();
        var cateuriesQuery = "select dbo.Categury.LatinGroupTitle from categury where Show=1";
        var categuries=DapperHelper.ExecuteCommand<List<SiteMapCategury>>(_connectionString,conn=>conn.Query<SiteMapCategury>(cateuriesQuery).ToList());
            
        var ProductsQuery = "select dbo.Product.ShortLink,dbo.ProductStockPrice.Id,dbo.Product.CreateDate from dbo.Product INNER JOIN dbo.ProductStockPrice ON DBO.ProductStockPrice.ProductId=DBO.Product.Id where dbo.Product.IsActive=1";
        var produtcs = DapperHelper.ExecuteCommand<List<SiteMapProducts>>(_connectionString, conn => conn.Query<SiteMapProducts>(ProductsQuery).ToList());

        var StoresQuery = "SELECT dbo.UserStore.LatinStoreName FROM UserStore";
        var stores = DapperHelper.ExecuteCommand<List<SiteMapStore>>(_connectionString, conn => conn.Query<SiteMapStore>(StoresQuery).ToList());


        foreach(var item in categuries)
        {
            SitemapDto sitemap = new()
            {
                loc=$"/Products/{item.LatinGroupTitle}",
                lastmod=DateTime.Now.ToString(),
                priority="0.90"
            };
            Result.Add(sitemap);
        }
        foreach (var item in produtcs)
        {
            SitemapDto sitemap = new()
            {
                loc = $"/Products/Detail/{item.ShortLink}/{item.Id}",
                lastmod =item.CreateDate.ToString() ,
                priority = "0.80"
            };
            Result.Add(sitemap);
        }

        foreach (var item in stores)
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