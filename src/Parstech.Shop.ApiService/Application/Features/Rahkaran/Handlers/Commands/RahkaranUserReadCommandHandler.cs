using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranUserReadCommandHandler : IRequestHandler<RahkaranUserReadCommandReq, RahkaranUserDto>
{
    private string _connectionString;


    public RahkaranUserReadCommandHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<RahkaranUserDto> Handle(RahkaranUserReadCommandReq request, CancellationToken cancellationToken)
    {
        string query = $"select* from dbo.RahkaranUser where dbo.RahkaranUser.UserId={request.id}";
        RahkaranUserDto item = DapperHelper.ExecuteCommand<RahkaranUserDto>(_connectionString,
            conn => conn.Query<RahkaranUserDto>(query).FirstOrDefault());
        return item;
    }
}