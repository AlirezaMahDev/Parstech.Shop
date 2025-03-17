using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.OrderStatus.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.OrderStatus.Handler.Queries;

public class
    GetOrderStatusByOrderIdQueryHandler : IRequestHandler<GetOrderStatusByOrderIdQueryReq, List<StatusOfOrderDto>>
{
    private readonly string _connectionString;

    public GetOrderStatusByOrderIdQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<List<StatusOfOrderDto>> Handle(GetOrderStatusByOrderIdQueryReq request,
        CancellationToken cancellationToken)
    {
        string sql =
            $"SELECT dbo.Orders.OrderId, dbo.OrderStatus.StatusId,dbo.OrderStatus.FileName, dbo.OrderStatus.CreateDate, dbo.Status.StatusName, dbo.OrderStatus.CreateBy FROM dbo.Orders INNER JOIN dbo.OrderStatus ON dbo.Orders.OrderId = dbo.OrderStatus.OrderId INNER JOIN dbo.Status ON dbo.OrderStatus.StatusId = dbo.Status.Id WHERE dbo.OrderStatus.OrderId={request.orderId}";
        List<StatusOfOrderDto> list =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<StatusOfOrderDto>(sql).ToList());
        foreach (StatusOfOrderDto? item in list)
        {
            item.CreateDateShamsi = item.CreateDate.ToShamsi();
        }

        return list;
    }
}