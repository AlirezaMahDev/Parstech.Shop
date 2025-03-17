using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

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

    Task<bool> OrderExistAnyDetails(int orderId);
    Task<Order?> GetOpenOrderOfUser(int userId);
}