using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Categury;
using Parstech.Shop.Context.Application.Features.Categury.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.Categury.Handlers.Queries;

public class CategurySelectListQueryHandler : IRequestHandler<CategurySelectListQueryReq, List<CategurySelectDto>>
{
    #region Constractor
    private readonly string _connectionString;

    public CategurySelectListQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    #endregion
    public async Task<List<CategurySelectDto>> Handle(CategurySelectListQueryReq request, CancellationToken cancellationToken)
    {
        var query = "select c.GroupId,c.GroupTitle,c.isParnet from Categury as c";
        return DapperHelper.ExecuteCommand<List<CategurySelectDto>>(_connectionString, conn => conn.Query<CategurySelectDto>(query).ToList());

    }
}