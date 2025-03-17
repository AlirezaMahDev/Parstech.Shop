namespace Parstech.Shop.ApiService.Application.Dapper.OrderDetail.Queries;

public interface IOrderDetailQueries
{
    string GetOrderDetailOfProductStockPriceId { get; }
    string GetOrderDetailsOfSaleStore { get; }
    string GetOrderDetailsOfAllSaleStore { get; }
}