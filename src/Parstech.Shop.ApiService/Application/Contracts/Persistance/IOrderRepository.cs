using Shop.Application.DTOs.Order;
using Shop.Application.DTOs.Paging;
using Shop.Application.DTOs.Product;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
    public interface IOrderRepository : IGenericRepository<Order>
    {

        public string GenerateWordOfOrder(OrderDetailShowDto order);
        Task OrderSum(int orderId);
        Task OrderDiscount(int orderId);
        Task OrderTax(int orderId);
        Task OrderTotal(int orderId);
        Task<Order> GetNotFinallyOrderOfUser(int userId);
        Task<List<OrderDto>> GetFinallyOrdersOfUser(int userId);
        Task<PagingDto> GetFinallyOrdersOfUserByPaging(int Pageid = 1, int Take = 30, string Filter = "", int userId = 0);

        Task<bool>OrderExistAnyDetails(int orderId);
        Task<Order?>GetOpenOrderOfUser(int userId);
        
        
    }
}
