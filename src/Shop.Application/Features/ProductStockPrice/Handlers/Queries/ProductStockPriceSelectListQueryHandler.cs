using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Product;
using Shop.Application.Features.ProductStockPrice.Requests.Queries;


namespace Shop.Application.Features.ProductStockPrice.Handlers.Queries
{
    
    public class ProductStockPriceSelectListQueryHandler : IRequestHandler<ProductStockPriceSelectListQueryReq, List<ProductSelectDto>>
    {
        #region Constractor
        private readonly string _connectionString;

        public ProductStockPriceSelectListQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        #endregion
        public async Task<List<ProductSelectDto>> Handle(ProductStockPriceSelectListQueryReq request, CancellationToken cancellationToken)
        {
            var condition = "";
            if (request.repId != 0) {
                condition = $"WHERE p.RepId={request.repId}";
            }
            var query =$"select p.Id, product.Name as ProductName, product.Code from ProductStockPrice as p inner join Product on p.ProductId=product.Id {condition}";
            return DapperHelper.ExecuteCommand<List<ProductSelectDto>>(_connectionString, conn => conn.Query<ProductSelectDto>(query).ToList());

        }
    }
}
