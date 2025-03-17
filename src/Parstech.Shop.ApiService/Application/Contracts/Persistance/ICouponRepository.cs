using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Contracts.Persistance;

public interface ICouponRepository : IGenericRepository<Coupon>
{
    Task<Coupon?> GetByCouponCode(string couponCode);
    OrderResponse CheckLimitUse(Coupon coupon);
    OrderResponse CheckExpireDate(Coupon coupon);
    OrderResponse CheckMinPrice(long Price, Coupon coupon);
    OrderResponse CheckMaxPrice(long Price, Coupon coupon);
    long CalculateDiscount(Coupon coupon, long Price);
    Task ChangeCoupon(OrderCoupon orderCoupon, Coupon coupon);
}