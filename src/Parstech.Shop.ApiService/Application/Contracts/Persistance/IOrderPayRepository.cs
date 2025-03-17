using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IOrderPayRepository : IGenericRepository<OrderPay>
{
    Task<OrderPay> GetByOrderId(int OrderId);
    Task<bool> HasOrderPay(int OrderId);
    Task<List<OrderPay>> GetListByOrderId(int OrderId);
}