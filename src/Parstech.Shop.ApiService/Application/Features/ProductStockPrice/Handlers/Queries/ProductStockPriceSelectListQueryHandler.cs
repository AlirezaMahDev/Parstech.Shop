using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;


namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Handlers.Queries;

public class
    ProductStockPriceSelectListQueryHandler : IRequestHandler<ProductStockPriceSelectListQueryReq,
    List<ProductSelectDto>>
{
    #region Constractor

    private readonly string _connectionString;

    public ProductStockPriceSelectListQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    #endregion

    public async Task<List<ProductSelectDto>> Handle(ProductStockPriceSelectListQueryReq request,
        CancellationToken cancellationToken)
    {
        string? query =
            $"select p.Id, product.Name as ProductName, product.Code from ProductStockPrice as p inner join Product on p.ProductId=product.Id WHERE p.RepId!={request.repId}";
        return DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<ProductSelectDto>(query).ToList());
    }
}