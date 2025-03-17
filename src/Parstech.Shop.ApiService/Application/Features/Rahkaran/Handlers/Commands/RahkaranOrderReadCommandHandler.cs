using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranOrderReadCommandHandler : IRequestHandler<RahkaranOrderReadCommandReq, RahkaranOrderDto>
{
    private string _connectionString;


    public RahkaranOrderReadCommandHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<RahkaranOrderDto> Handle(RahkaranOrderReadCommandReq request, CancellationToken cancellationToken)
    {
        string query = $"select* from dbo.RahkaranOrder where dbo.RahkaranOrder.OrderId={request.id}";
        RahkaranOrderDto item = DapperHelper.ExecuteCommand<RahkaranOrderDto>(_connectionString,
            conn => conn.Query<RahkaranOrderDto>(query).FirstOrDefault());
        return item;
    }
}