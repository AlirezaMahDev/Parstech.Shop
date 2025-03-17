using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Product.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Product.Handlers.Queries;

public class ProductSelectListQueryHandler : IRequestHandler<ProductSelectListQueryReq, List<ProductSelectDto>>
{
    #region Constractor

    private readonly string _connectionString;

    public ProductSelectListQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    #endregion

    public async Task<List<ProductSelectDto>> Handle(ProductSelectListQueryReq request,
        CancellationToken cancellationToken)
    {
        string query = "select p.Id,p.Code, p.Name as ProductName from Product as p WHERE p.TypeId!=3";
        return DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<ProductSelectDto>(query).ToList());
    }
}