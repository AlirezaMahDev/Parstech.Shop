using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.User.Queries;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.User.Requests.Queries;

namespace Parstech.Shop.ApiService.Application.Features.User.Handlers.Queries;

public class
    GetUsersForSelectListQueryHandler : IRequestHandler<GetUsersForSelectListQueryReq, List<UserForSelectListDto>>
{
    private readonly IUserQueries _userQueries;
    private readonly string _connectionString;

    public GetUsersForSelectListQueryHandler(IUserQueries userQueries, IConfiguration configuration)
    {
        _userQueries = userQueries;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<UserForSelectListDto>> Handle(GetUsersForSelectListQueryReq request,
        CancellationToken cancellationToken)
    {
        List<UserForSelectListDto>? list = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<UserForSelectListDto>(_userQueries.GetAllUsers).ToList());
        return list;
    }
}