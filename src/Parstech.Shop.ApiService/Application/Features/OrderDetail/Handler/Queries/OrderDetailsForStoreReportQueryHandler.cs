using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Shop.Application.Convertor;
using Shop.Application.Dapper.Helper;
using Shop.Application.Dapper.OrderDetail.Queries;
using Shop.Application.Dapper.WalletTransaction.Queries;
using Shop.Application.DTOs.OrderDetail;
using Shop.Application.DTOs.WalletTransaction;
using Shop.Application.Features.OrderDetail.Requests.Queries;
using Shop.Application.Features.WalletTransaction.Requests.Queries;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Features.OrderDetail.Handler.Queries
{


    public class OrderDetailsForStoreReportQueryHandler : IRequestHandler<OrderDetailsForStoreReportQueryReq, SalesPagingDto>
    {
        private readonly IOrderDetailQueries _orderDetailsQuery;
        private readonly string _connectionString;
        public OrderDetailsForStoreReportQueryHandler(IOrderDetailQueries orderDetailsQuery, IConfiguration configuration)
        {
            _orderDetailsQuery = orderDetailsQuery;
            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
        public async Task<SalesPagingDto> Handle(OrderDetailsForStoreReportQueryReq request, CancellationToken cancellationToken)
        {
            SalesPagingDto result = new SalesPagingDto();
            int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;

            var list = new List<OrderDetailSaleDto>();
            if (request.Admin)
            {
                list = DapperHelper.ExecuteCommand<List<OrderDetailSaleDto>>(_connectionString, conn => conn.Query<OrderDetailSaleDto>(_orderDetailsQuery.GetOrderDetailsOfAllSaleStore).ToList());
            }
            else
            {
                var query = $"SELECT dbo.OrderDetail.Count,dbo.OrderDetail.DetailSum,dbo.OrderDetail.Discount,dbo.OrderDetail.Price,dbo.OrderDetail.Tax,dbo.OrderDetail.Total,dbo.Orders.OrderId,dbo.Orders.OrderCode,dbo.Orders.CreateDate,dbo.ProductStockPrice.Id As ProductStockPriceId,dbo.Product.Name,dbo.ProductStockPrice.StoreId,dbo.UserStore.StoreName FROM dbo.OrderDetail INNER JOIN dbo.Orders on .dbo.OrderDetail.OrderId=dbo.Orders.OrderId INNER JOIN dbo.ProductStockPrice on .dbo.OrderDetail.ProductStockPriceId=dbo.ProductStockPrice.Id INNER JOIN dbo.Product on .dbo.ProductStockPrice.ProductId=dbo.Product.Id INNER JOIN dbo.UserStore on dbo.ProductStockPrice.StoreId=dbo.UserStore.Id WHERE dbo.ProductStockPrice.StoreId={request.parameter.StoreId}";

                list = DapperHelper.ExecuteCommand<List<OrderDetailSaleDto>>(_connectionString, conn => conn.Query<OrderDetailSaleDto>(query).ToList());
            }
            foreach (var item in list)
            {
                item.CreateDateShamsi = item.CreateDate.ToShamsi();

            }

            if (request.parameter.StoreId != 0)
            {
                list = list.Where(u => u.StoreId == request.parameter.StoreId).ToList();
            }





            if (!string.IsNullOrEmpty(request.parameter.FromDate))
            {
                request.parameter.FromDate = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.parameter.FromDate);
                string[] std = request.parameter.FromDate.Split('/');
                var az = new DateTime(int.Parse(std[0]),
                    int.Parse(std[1]),
                int.Parse(std[2]),
                    new PersianCalendar()
                );
                list = list.Where(p => (p.CreateDate >= az)).ToList();
            }
            if (!string.IsNullOrEmpty(request.parameter.ToDate))
            {
                request.parameter.ToDate = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.parameter.ToDate);
                string[] edd = request.parameter.ToDate.Split('/');
                var ta = new DateTime(int.Parse(edd[0]),
                    int.Parse(edd[1]),
                    int.Parse(edd[2]),
                    new PersianCalendar()
                );
                list = list.Where(p => (p.CreateDate <= ta)).ToList();
            }

            if (request.parameter.TakePage == -1)
            {
                result.sales = list;
                return result;
            }
            result.CurrentPage = request.parameter.CurrentPage;

            result.PageCount = (list.Count() / request.parameter.TakePage) + 1;


            result.List = list.Skip(skip).Take(request.parameter.TakePage).ToArray();
            return result;
        }
    }
}
