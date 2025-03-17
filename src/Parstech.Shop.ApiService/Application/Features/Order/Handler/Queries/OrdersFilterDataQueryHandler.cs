using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Features.Order.Requests.Queries;
using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Features.Order.Handler.Queries;

public class OrdersFilterDataQueryHandler : IRequestHandler<OrdersFilterDataQueryReq, OrderFilterDto>
{
    private readonly string _connectionString;

    public OrdersFilterDataQueryHandler(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<OrderFilterDto> Handle(OrdersFilterDataQueryReq request, CancellationToken cancellationToken)
    {
        OrderFilterDto response = new();

        string storeQuery = "";
        if (request.userName != null)
        {
            storeQuery = $"WHERE dbo.[User].UserName='{request.userName}'";
        }

        string store =
            $"SELECT dbo.UserStore.StoreName , dbo.UserStore.Id as UserStoreId ,dbo.[User].Id as UserId,dbo.[User].UserName from dbo.UserStore inner join dbo.[User] on dbo.UserStore.UserId=dbo.[User].Id {storeQuery}";
        response.stores =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<storeFilterDto>(store).ToList());
        string status = "SELECT dbo.Status.StatusName , dbo.Status.Id from dbo.Status";
        response.statuses =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<statusFilterDto>(status).ToList());
        statusFilterDto statusFilterDto = new() { Id = 999, StatusName = "سبد خرید" };
        response.statuses.Add(statusFilterDto);


        string pay = "SELECT dbo.PayType.TypeName , dbo.PayType.Id from dbo.PayType";
        response.pays = DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<payFilterDto>(pay).ToList());
        payFilterDto payFilterDto = new() { Id = 999, TypeName = "نا مشخص" };
        response.pays.Add(payFilterDto);

        string ordercode = "SELECT dbo.Orders.OrderCode from dbo.Orders";
        response.ordercodes = DapperHelper.ExecuteCommand(_connectionString,
            conn => conn.Query<ordercodeFilterDto>(ordercode).ToList());
        string customers =
            "SELECT dbo.[User].Id,dbo.UserBilling.FirstName,dbo.UserBilling.LastName FROM dbo.Orders left JOIN dbo.[User] ON dbo.Orders.UserId = dbo.[User].Id inner JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId";
        response.customers =
            DapperHelper.ExecuteCommand(_connectionString, conn => conn.Query<customerFilterDto>(customers).ToList());
        return response;
    }
}