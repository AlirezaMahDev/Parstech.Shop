using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Parstech.Shop.Context.Application.Convertor;
using Parstech.Shop.Context.Application.Dapper.Helper;
using Parstech.Shop.Context.Application.DTOs.OrderStatus;
using Parstech.Shop.Context.Application.Features.OrderStatus.Requests.Queries;

namespace Parstech.Shop.Context.Application.Features.OrderStatus.Handler.Queries;

public class GetOrderStatusByOrderIdQueryHandler : IRequestHandler<GetOrderStatusByOrderIdQueryReq, List<StatusOfOrderDto>>
{
    private readonly string _connectionString;
    public GetOrderStatusByOrderIdQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
            
    }
    public async Task<List<StatusOfOrderDto>> Handle(GetOrderStatusByOrderIdQueryReq request, CancellationToken cancellationToken)
    {
        var sql = $"SELECT dbo.Orders.OrderId, dbo.OrderStatus.StatusId,dbo.OrderStatus.FileName, dbo.OrderStatus.CreateDate, dbo.Status.StatusName, dbo.OrderStatus.CreateBy FROM dbo.Orders INNER JOIN dbo.OrderStatus ON dbo.Orders.OrderId = dbo.OrderStatus.OrderId INNER JOIN dbo.Status ON dbo.OrderStatus.StatusId = dbo.Status.Id WHERE dbo.OrderStatus.OrderId={request.orderId}";
        var list=DapperHelper.ExecuteCommand<List<StatusOfOrderDto>>(_connectionString, conn => conn.Query<StatusOfOrderDto>(sql).ToList());
        foreach(var item in list)
        {
            item.CreateDateShamsi = item.CreateDate.ToShamsi();
        }
        return list;
    }
}