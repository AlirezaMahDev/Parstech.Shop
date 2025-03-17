using Dapper;

using MediatR;

using Parstech.Shop.ApiService.Application.Convertor;
using Parstech.Shop.ApiService.Application.Dapper.Helper;
using Parstech.Shop.ApiService.Application.Dapper.OrderDetail.Queries;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Application.Features.OrderDetail.Requests.Queries;

using System.Globalization;

namespace Parstech.Shop.ApiService.Application.Features.OrderDetail.Handler.Queries;

public class
    OrderDetailsForStoreReportQueryHandler : IRequestHandler<OrderDetailsForStoreReportQueryReq, SalesPagingDto>
{
    private readonly IOrderDetailQueries _orderDetailsQuery;
    private readonly string _connectionString;

    public OrderDetailsForStoreReportQueryHandler(IOrderDetailQueries orderDetailsQuery, IConfiguration configuration)
    {
        _orderDetailsQuery = orderDetailsQuery;
        _connectionString = configuration.GetConnectionString("DatabaseConnection");
    }

    public async Task<SalesPagingDto> Handle(OrderDetailsForStoreReportQueryReq request,
        CancellationToken cancellationToken)
    {
        SalesPagingDto result = new();
        int skip = (request.parameter.CurrentPage - 1) * request.parameter.TakePage;

        List<OrderDetailSaleDto> list = new();
        if (request.Admin)
        {
            list = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<OrderDetailSaleDto>(_orderDetailsQuery.GetOrderDetailsOfAllSaleStore).ToList());
        }
        else
        {
            string query =
                $"SELECT dbo.OrderDetail.Count,dbo.OrderDetail.DetailSum,dbo.OrderDetail.Discount,dbo.OrderDetail.Price,dbo.OrderDetail.Tax,dbo.OrderDetail.Total,dbo.Orders.OrderId,dbo.Orders.OrderCode,dbo.Orders.CreateDate,dbo.ProductStockPrice.Id As ProductStockPriceId,dbo.Product.Name,dbo.ProductStockPrice.StoreId,dbo.UserStore.StoreName FROM dbo.OrderDetail INNER JOIN dbo.Orders on .dbo.OrderDetail.OrderId=dbo.Orders.OrderId INNER JOIN dbo.ProductStockPrice on .dbo.OrderDetail.ProductStockPriceId=dbo.ProductStockPrice.Id INNER JOIN dbo.Product on .dbo.ProductStockPrice.ProductId=dbo.Product.Id INNER JOIN dbo.UserStore on dbo.ProductStockPrice.StoreId=dbo.UserStore.Id WHERE dbo.ProductStockPrice.StoreId={request.parameter.StoreId}";

            list = DapperHelper.ExecuteCommand(_connectionString,
                conn => conn.Query<OrderDetailSaleDto>(query).ToList());
        }

        foreach (OrderDetailSaleDto item in list)
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
            DateTime az = new(int.Parse(std[0]),
                int.Parse(std[1]),
                int.Parse(std[2]),
                new PersianCalendar()
            );
            list = list.Where(p => p.CreateDate >= az).ToList();
        }

        if (!string.IsNullOrEmpty(request.parameter.ToDate))
        {
            request.parameter.ToDate = ConvertPersianNumbersToEnglish.ToEnglishNumber(request.parameter.ToDate);
            string[] edd = request.parameter.ToDate.Split('/');
            DateTime ta = new(int.Parse(edd[0]),
                int.Parse(edd[1]),
                int.Parse(edd[2]),
                new PersianCalendar()
            );
            list = list.Where(p => p.CreateDate <= ta).ToList();
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