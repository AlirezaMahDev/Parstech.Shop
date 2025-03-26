using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IOrderPayRepository:IGenericRepository<OrderPay>
{
    Task<OrderPay> GetByOrderId(int OrderId);
    Task<bool> HasOrderPay(int OrderId);
    Task<List<OrderPay>> GetListByOrderId(int OrderId);
}