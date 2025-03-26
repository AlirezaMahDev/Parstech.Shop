using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranOrderReadCommandHandler : IRequestHandler<RahkaranOrderReadCommandReq, RahkaranOrderDto>
{

    private string _connectionString;
        

    public RahkaranOrderReadCommandHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<RahkaranOrderDto> Handle(RahkaranOrderReadCommandReq request, CancellationToken cancellationToken)
    {
        var query = $"select* from dbo.RahkaranOrder where dbo.RahkaranOrder.OrderId={request.id}";
        var item=DapperHelper.ExecuteCommand<RahkaranOrderDto>(_connectionString, conn=>conn.Query<RahkaranOrderDto>(query).FirstOrDefault());
        return item;
    }
}