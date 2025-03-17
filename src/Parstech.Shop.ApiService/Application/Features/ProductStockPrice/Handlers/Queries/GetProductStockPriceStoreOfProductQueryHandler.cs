using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.ProductStockPrice.Handlers.Queries;

public class GetProductStockPriceStoreOfProductQueryHandler : IRequestHandler<GetProductStockPriceStoreOfProductQueryReq
    , List<ProductStockPriceStoreDto>>
{
    private readonly string _connectionString;

    public GetProductStockPriceStoreOfProductQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<ProductStockPriceStoreDto>> Handle(GetProductStockPriceStoreOfProductQueryReq request,
        CancellationToken cancellationToken)
    {
        string? sql =
            $"SELECT dbo.ProductStockPrice.Id, dbo.UserStore.StoreName FROM dbo.ProductStockPrice INNER JOIN dbo.UserStore ON dbo.ProductStockPrice.StoreId = dbo.UserStore.Id where ProductId={request.ProductId}";
        List<ProductStockPriceStoreDto>? result = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<ProductStockPriceStoreDto>(sql).ToList());

        return result;
    }
}