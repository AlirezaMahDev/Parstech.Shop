using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface IOrderCouponRepository : IGenericRepository<OrderCoupon>
{
    Task<bool> CouponExistInOrder(int orderId, int couponId);
    Task<bool> CouponExistInOrderCoupon(int couponId);
    Task<bool> OrderHaveCoupon(int orderId);
    Task<OrderCoupon?> GetByOrderId(int orderId);
    Task CheckAndDelete(int orderId);
    Task<bool> ExistInOrder(int orderId);
}