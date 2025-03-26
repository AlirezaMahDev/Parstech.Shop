using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
{
    Task<List<OrderDetail>> GetOrderDetailsByOrderId(int orderId);
    Task CalculateOrderDetailTax(int orderId);
    Task<int> CountOfSaleByProductId(int productId);

    Task<OrderDetail> RefreshOrderDetail(int detailId);
    Task<bool> ProductIdExistInOrderDetails(int orderId, int productId);

    Task<int> GetCountOfOrder(int orderId);

    Task<bool> ExistOrderDetailforProductStockPrice(int ProductStockPricId);
}