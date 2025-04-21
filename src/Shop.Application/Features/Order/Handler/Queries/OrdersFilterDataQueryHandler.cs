using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Dapper.Helper;
using Shop.Application.DTOs.Paging;
using Shop.Application.Features.Order.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.Order.Handler.Queries
{
	public class OrdersFilterDataQueryHandler : IRequestHandler<OrdersFilterDataQueryReq, OrderFilterDto>
	{
		private readonly string _connectionString;
        public OrdersFilterDataQueryHandler(IConfiguration configuration)
        {
			_connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<OrderFilterDto> Handle(OrdersFilterDataQueryReq request, CancellationToken cancellationToken)
		{
			OrderFilterDto response=new OrderFilterDto();

			var storeQuery = "";
			if (request.userName != null)
			{
				storeQuery = $"WHERE dbo.[User].UserName='{request.userName}'";
			}
			var store = $"SELECT dbo.UserStore.StoreName , dbo.UserStore.Id as UserStoreId ,dbo.[User].Id as UserId,dbo.[User].UserName from dbo.UserStore inner join dbo.[User] on dbo.UserStore.UserId=dbo.[User].Id {storeQuery}";
			response.stores= DapperHelper.ExecuteCommand<List<storeFilterDto>>(_connectionString, conn => conn.Query<storeFilterDto>(store).ToList());
			var status = "SELECT s.StatusName , s.Id from dbo.Status as s where s.Id!=2 and s.Id!=6";
			response.statuses = DapperHelper.ExecuteCommand<List<statusFilterDto>>(_connectionString, conn => conn.Query<statusFilterDto>(status).ToList());
			statusFilterDto statusFilterDto = new statusFilterDto()
			{
				Id = 999,
				StatusName = "سبد خرید"
			};
			response.statuses.Add(statusFilterDto);


			var pay = "SELECT dbo.PayType.TypeName , dbo.PayType.Id from dbo.PayType";
			response.pays = DapperHelper.ExecuteCommand<List<payFilterDto>>(_connectionString, conn => conn.Query<payFilterDto>(pay).ToList());
			payFilterDto payFilterDto = new payFilterDto()
			{
				Id = 999,
				TypeName = "نا مشخص"
			};
			response.pays.Add(payFilterDto);

			var ordercode = "SELECT dbo.Orders.OrderCode from dbo.Orders";
			response.ordercodes = DapperHelper.ExecuteCommand<List<ordercodeFilterDto>>(_connectionString, conn => conn.Query<ordercodeFilterDto>(ordercode).ToList());
			var customers = "SELECT dbo.[User].Id,dbo.UserBilling.FirstName,dbo.UserBilling.LastName FROM dbo.Orders left JOIN dbo.[User] ON dbo.Orders.UserId = dbo.[User].Id inner JOIN dbo.UserBilling ON dbo.[User].Id = dbo.UserBilling.UserId";
			response.customers = DapperHelper.ExecuteCommand<List<customerFilterDto>>(_connectionString, conn => conn.Query<customerFilterDto>(customers).ToList());
			return response;
		}
	}
}
