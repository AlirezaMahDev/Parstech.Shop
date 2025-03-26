using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Contracts.Persistance;

public interface ICouponPcuRepository:IGenericRepository<CouponPcu>
{
    Task<List<CouponPcu>> GetPCUOfCoupon(string Type, bool Status, Coupon coupon);
		
}