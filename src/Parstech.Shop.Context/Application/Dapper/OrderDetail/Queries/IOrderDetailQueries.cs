namespace Parstech.Shop.Context.Application.Dapper.OrderDetail.Queries;

public interface IOrderDetailQueries
{
    string GetOrderDetailOfProductStockPriceId { get; }
    string GetOrderDetailsOfSaleStore { get; }
    string GetOrderDetailsOfAllSaleStore { get; }
}