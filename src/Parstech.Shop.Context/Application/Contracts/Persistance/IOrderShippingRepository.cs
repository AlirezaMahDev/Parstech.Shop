using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface IOrderShippingRepository : IGenericRepository<OrderShipping>
{
    Task<OrderShipping> GetOrderShippingByOrderId(int orderId);
}