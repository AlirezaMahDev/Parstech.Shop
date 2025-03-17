using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Rahkaran.Requests.Commands;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranProductReadCommandHandler : IRequestHandler<RahkaranProductReadCommandReq, RahkaranProductDto>
{
    private string _connectionString;


    public RahkaranProductReadCommandHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<RahkaranProductDto> Handle(RahkaranProductReadCommandReq request,
        CancellationToken cancellationToken)
    {
        string query = $"select* from dbo.RahkaranProduct where dbo.RahkaranProduct.ProductId={request.id}";
        RahkaranProductDto item = DapperHelper.ExecuteCommand<RahkaranProductDto>(_connectionString,
            conn => conn.Query<RahkaranProductDto>(query).FirstOrDefault());
        return item;
    }
}