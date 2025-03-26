using Dapper;

using MediatR;

using Microsoft.Extensions.Configuration;

using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.Rahkaran;
using Parstech.Shop.Context.Application.Features.Rahkaran.Requests.Commands;

namespace Parstech.Shop.Context.Application.Features.Rahkaran.Handlers.Commands;

public class RahkaranProductReadCommandHandler : IRequestHandler<RahkaranProductReadCommandReq, RahkaranProductDto>
{



    private string _connectionString;


    public RahkaranProductReadCommandHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }


    public async Task<RahkaranProductDto> Handle(RahkaranProductReadCommandReq request, CancellationToken cancellationToken)
    {
        var query = $"select* from dbo.RahkaranProduct where dbo.RahkaranProduct.ProductId={request.id}";
        var item = DapperHelper.ExecuteCommand<RahkaranProductDto>(_connectionString, conn => conn.Query<RahkaranProductDto>(query).FirstOrDefault());
        return item;
    }
}