using System.Data;
using System.Data.SqlClient;

using Dapper;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Dapper.ProductProperty.Queries;

public class GetProductPropertiesByParrentIdQueryHandler
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public GetProductPropertiesByParrentIdQueryHandler(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DB");
    }

    public async Task<List<PropertyListDto>> ExecuteAsync(long productId)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        var query = @"
            SELECT
                pcp.Id,
                pcp.CreateDay,
                pcp.Status,
                pcp.UpdateDay,
                pcp.ParrentId,
                pcp.IsEnable,
                pcp.Name AS Title,
                pcp.Value,
                pcp.OrderShow
            FROM
                ProductCategoryProperty pcp
            WHERE
                pcp.ParrentId = @ProductId
            ORDER BY
                pcp.OrderShow ASC";

        var parameters = new DynamicParameters();
        parameters.Add("@ProductId", productId, DbType.Int64);

        var result = await connection.QueryAsync<PropertyListDto>(query, parameters);
        return result.ToList();
    }
} 