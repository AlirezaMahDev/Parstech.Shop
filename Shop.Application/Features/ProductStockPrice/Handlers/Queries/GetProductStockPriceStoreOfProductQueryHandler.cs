using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.ProductStockPrice.Handlers.Queries
{
    public class GetProductStockPriceStoreOfProductQueryHandler : IRequestHandler<GetProductStockPriceStoreOfProductQueryReq, List<ProductStockPriceStoreDto>>
    {
        private readonly string _connectionString;
        public GetProductStockPriceStoreOfProductQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<List<ProductStockPriceStoreDto>> Handle(GetProductStockPriceStoreOfProductQueryReq request, CancellationToken cancellationToken)
        {
            var sql = $"SELECT dbo.ProductStockPrice.Id, dbo.UserStore.StoreName FROM dbo.ProductStockPrice INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id where ProductId={request.ProductId}";
            var result =DapperHelper.ExecuteCommand<List<ProductStockPriceStoreDto>>(_connectionString,conn=>conn.Query<ProductStockPriceStoreDto>(sql).ToList());

            return result;
        }
    }
}
