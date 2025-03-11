using Dapper;
using Dto.Response.Payment;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.Product.Queries;
using Shop.Application.DTOs.Api;
using Shop.Application.DTOs.SiteMap;
using Shop.Application.Features.Api.Requests.Queries;
using Shop.Application.Url;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Api.Handlers.Queries
{
    public class TorobGetProductsQueryHandler : IRequestHandler<TorobGetProductsQueryReq, List<TorobProductDto>>
    {
        private readonly string _connectionString;
       
        public TorobGetProductsQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
            
        }
        public async Task<List<TorobProductDto>> Handle(TorobGetProductsQueryReq request, CancellationToken cancellationToken)
        {
            
            int skip = (request.page - 1) * 100;

            var query = $"SELECT dbo.Product.Name, dbo.ProductStockPrice.Id,dbo.ProductStockPrice.DiscountDate, dbo.ProductStockPrice.SalePrice, dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.Quantity, dbo.Product.ShortLink FROM  dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId ORDER BY dbo.Product.CreateDate Desc OFFSET {skip} ROWS FETCH NEXT 100 ROWS ONLY";
            var result = DapperHelper.ExecuteCommand<List<TorobProductDto>> (_connectionString, conn => conn.Query<TorobProductDto>(query).ToList());
            foreach ( var item in result )
            {
                if (item.DiscountDate != null && item.DiscountDate>=DateTime.Now)
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

           

            var query = $"SELECT dbo.Product.Name,dbo.Product.ParentId,dbo.Product.TypeId,dbo.Product.ShortDescription,dbo.Product.Id as ProductId, dbo.ProductStockPrice.Id,dbo.ProductStockPrice.DiscountDate, dbo.ProductStockPrice.SalePrice, dbo.ProductStockPrice.DiscountPrice, dbo.ProductStockPrice.Quantity, dbo.Product.ShortLink FROM  dbo.Product INNER JOIN dbo.ProductStockPrice ON dbo.Product.Id = dbo.ProductStockPrice.ProductId Where dbo.ProductStockPrice.Id= {request.productId}";
            var item = DapperHelper.ExecuteCommand<TorobProductDto>(_connectionString, conn => conn.Query<TorobProductDto>(query).FirstOrDefault());
            var productId = 0;
            
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

            var imgQuery = DapperHelper.ExecuteCommand<Domain.Models.ProductGallery>(_connectionString, conn => conn.Query<Domain.Models.ProductGallery>(_productQueries.GetMainImage, new { @productId = productId }).FirstOrDefault());
            if (imgQuery != null)
            {
                image = imgQuery.ImageName;
            }


            TorobDto torobItem = new TorobDto()
            {
                product_id = item.Id.ToString(),
                page_url = $"{request.url}/Products/Detail/{item.ShortLink}/{item.Id}",
                price = price,
                old_price = Oldprice,
                availability = availability,
                Image =$"{request.url}/Shared/Images/Products/{image}",
                Content=item.ShortDescription,
                Name= item.Name,
            };
            return torobItem;
        }
    }
}
