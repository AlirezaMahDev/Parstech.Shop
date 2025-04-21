using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.OrderDetail;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
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
}
