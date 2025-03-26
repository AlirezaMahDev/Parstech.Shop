using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.Dapper.User.Queries;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.User.Handlers.Queries;

public class GetUsersForSelectListQueryHandler : IRequestHandler<GetUsersForSelectListQueryReq, List<UserForSelectListDto>>
{
    private readonly IUserQueries _userQueries;
    private readonly string _connectionString;
    public GetUsersForSelectListQueryHandler(IUserQueries userQueries,IConfiguration configuration)
    {
        _userQueries=userQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }
    public async Task<List<UserForSelectListDto>> Handle(GetUsersForSelectListQueryReq request, CancellationToken cancellationToken)
    {
            
        var list = DapperHelper.ExecuteCommand<List<UserForSelectListDto>>(_connectionString, conn => conn.Query<UserForSelectListDto>(_userQueries.GetAllUsers).ToList());
        return list;
    }
}