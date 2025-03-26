using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Queries;

public class UserFilterDataQueryHandler : IRequestHandler<UserFilterDataQueryReq, List<UserFilterDto>>
{
    private readonly string _connectionString;
    public UserFilterDataQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");   
    }
    public async Task<List<UserFilterDto>> Handle(UserFilterDataQueryReq request, CancellationToken cancellationToken)
    {
        var query = "select dbo.[User].Id,dbo.[User].UserName,dbo.UserBilling.FirstName,dbo.UserBilling.LastName,dbo.UserBilling.EconomicCode,dbo.UserBilling.NationalCode,dbo.UserBilling.Mobile from dbo.[User] inner join dbo.UserBilling on dbo.[User].Id=dbo.UserBilling.UserId";
        var res = DapperHelper.ExecuteCommand<List<UserFilterDto>>(_connectionString, conn => conn.Query<UserFilterDto>(query).ToList());
        return res;

    }
}