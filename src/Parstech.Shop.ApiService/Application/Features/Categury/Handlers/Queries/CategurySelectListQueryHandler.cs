using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Categury.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Categury.Handlers.Queries;

public class CategurySelectListQueryHandler : IRequestHandler<CategurySelectListQueryReq, List<CategurySelectDto>>
{
    #region Constractor

    private readonly string _connectionString;

    public CategurySelectListQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    #endregion

    public async Task<List<CategurySelectDto>> Handle(CategurySelectListQueryReq request,
        CancellationToken cancellationToken)
    {
        string query = "select c.GroupId,c.GroupTitle,c.isParnet from Categury as c";
        return DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<CategurySelectDto>(query).ToList());
    }
}