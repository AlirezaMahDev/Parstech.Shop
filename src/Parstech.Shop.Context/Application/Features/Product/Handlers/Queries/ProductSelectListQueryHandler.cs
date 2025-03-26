using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.Features.Product.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Product.Handlers.Queries;

public class ProductSelectListQueryHandler : IRequestHandler<ProductSelectListQueryReq, List<ProductSelectDto>>
{
    #region Constractor
    private readonly string _connectionString;

    public ProductSelectListQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    #endregion
    public async Task<List<ProductSelectDto>> Handle(ProductSelectListQueryReq request, CancellationToken cancellationToken)
    {
        var query = "select p.Id,p.Code, p.Name as ProductName from Product as p WHERE p.TypeId!=3";
        return DapperHelper.ExecuteCommand<List<ProductSelectDto>>(_connectionString, conn => conn.Query<ProductSelectDto>(query).ToList());
        
    }
}