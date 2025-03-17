using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class UserFilterDataQueryHandler : IRequestHandler<UserFilterDataQueryReq, List<UserFilterDto>>
{
    private readonly string _connectionString;

    public UserFilterDataQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<UserFilterDto>> Handle(UserFilterDataQueryReq request, CancellationToken cancellationToken)
    {
        string query =
            "select dbo.[User].Id,dbo.[User].UserName,dbo.UserBilling.FirstName,dbo.UserBilling.LastName,dbo.UserBilling.EconomicCode,dbo.UserBilling.NationalCode,dbo.UserBilling.Mobile from dbo.[User] inner join dbo.UserBilling on dbo.[User].Id=dbo.UserBilling.UserId";
        List<UserFilterDto> res =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<UserFilterDto>(query).ToList());
        return res;
    }
}