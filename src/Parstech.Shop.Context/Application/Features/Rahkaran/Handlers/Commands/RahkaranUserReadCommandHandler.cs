using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranUserReadCommandHandler : IRequestHandler<RahkaranUserReadCommandReq, RahkaranUserDto>
{

    private string _connectionString;


    public RahkaranUserReadCommandHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<RahkaranUserDto> Handle(RahkaranUserReadCommandReq request, CancellationToken cancellationToken)
    {
        var query = $"select* from dbo.RahkaranUser where dbo.RahkaranUser.UserId={request.id}";
        var item = DapperHelper.ExecuteCommand<RahkaranUserDto>(_connectionString, conn => conn.Query<RahkaranUserDto>(query).FirstOrDefault());
        return item;
    }
}