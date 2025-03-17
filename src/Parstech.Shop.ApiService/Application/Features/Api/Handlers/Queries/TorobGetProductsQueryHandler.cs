using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.Product.Queries;
using Parstech.Shop.ApiService.Application.Features.Api.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Api.Handlers.Queries;

public class TorobGetProductsQueryHandler : IRequestHandler<TorobGetProductsQueryReq, List<TorobProductDto>>
{
    private readonly string _connectionString;

    public TorobGetProductsQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<TorobProductDto>> Handle(TorobGetProductsQueryReq request,
        CancellationToken cancellationToken)
    {
        int skip = (request.page - 1) * 100;

        string query =
            $"SELECT dbo.Product.Name, dbo.ProductStockPrice.Id,dbo.ProductStockPrice.DiscountDate, dbo.ProductStockPrice.SalePrice, dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.Quantity, dbo.Product.ShortLink FROM  dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId ORDER BY dbo.Product.CreateDate Desc OFFSET {skip} ROWS FETCH NEXT 100 ROWS ONLY";
        List<TorobProductDto> result =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<TorobProductDto>(query).ToList());
        foreach (TorobProductDto? item in result)
        {
            if (item.DiscountDate != null && item.DiscountDate >= DateTime.Now)
            {
            }
            else
            {
                item.DiscountPrice = 0;
            }
        }

        return result;
    }
}

//Get One
public class TorobGetProductQueryHandler : IRequestHandler<TorobGetProductQueryReq, TorobDto>
{
    private readonly string _connectionString;
    private readonly IProductQueries _productQueries;

    public TorobGetProductQueryHandler(IConfiguration configuration, IProductQueries productQueries)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
        _productQueries = productQueries;
    }

    public async Task<TorobDto> Handle(TorobGetProductQueryReq request, CancellationToken cancellationToken)
    {
        string query =
            $"SELECT dbo.Product.Name,dbo.Product.ParentId,dbo.Product.TypeId,dbo.Product.ShortDescription,dbo.Product.Id as ProductId, dbo.ProductStockPrice.Id,dbo.ProductStockPrice.DiscountDate, dbo.ProductStockPrice.SalePrice, dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.Quantity, dbo.Product.ShortLink FROM  dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId Where dbo.ProductStockPrice.Id= {request.productId}";
        TorobProductDto item = DapperHelper.ExecuteCommand<TorobProductDto>(_connectionString,
            conn => conn.Query<TorobProductDto>(query).FirstOrDefault());
        int productId = 0;

        if (item.ParentId != null)
        {
            productId = item.ParentId.Value;
        }
        else
        {
            productId = item.ProductId;
        }

        if (item.DiscountDate != null && item.DiscountDate >= DateTime.Now)
        {
        }
        else
        {
            item.DiscountPrice = 0;
        }


        string price = "";
        string Oldprice = "";
        string availability = "";
        string image = "";

        if (item.Quantity > 0 && item.SalePrice > 0)
        {
            availability = "instock";
        }
        else
        {
            availability = "outofstock";
        }

        if (item.DiscountPrice > 0)
        {
            price = item.DiscountPrice.ToString();
            Oldprice = item.SalePrice.ToString();
        }
        else
        {
            price = item.SalePrice.ToString();
            Oldprice = item.SalePrice.ToString();
        }

        Shared.Models.ProductGallery imgQuery = DapperHelper.ExecuteCommand<Shared.Models.ProductGallery>(
            _connectionString,
            conn => conn.Query<Shared.Models.ProductGallery>(_productQueries.GetMainImage, new { productId })
                .FirstOrDefault());
        if (imgQuery != null)
        {
            image = imgQuery.ImageName;
        }


        TorobDto torobItem = new()
        {
            product_id = item.Id.ToString(),
            page_url = $"{request.url}/Products/Detail/{item.ShortLink}/{item.Id}",
            price = price,
            old_price = Oldprice,
            availability = availability,
            Image = $"{request.url}/Shared/Images/Products/{image}",
            Content = item.ShortDescription,
            Name = item.Name
        };
        return torobItem;
    }
}