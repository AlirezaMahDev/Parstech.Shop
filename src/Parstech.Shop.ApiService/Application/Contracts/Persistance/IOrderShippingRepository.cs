using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IOrderShippingRepository : IGenericRepository<OrderShipping>
{
    Task<OrderShipping> GetOrderShippingByOrderId(int orderId);
}