using Shop.Application.DTOs.Categury;
using Shop.Application.DTOs.Coupon;
using Shop.Application.DTOs.Paging;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
	public interface ICouponRepository:IGenericRepository<Coupon>
	{
		Task<Coupon?> GetByCouponCode(string couponCode);
		OrderResponse CheckLimitUse(Coupon coupon);
		OrderResponse CheckExpireDate(Coupon coupon);
		OrderResponse CheckMinPrice(long Price, Coupon coupon);
		OrderResponse CheckMaxPrice(long Price, Coupon coupon);
		long CalculateDiscount(Coupon coupon, long Price);
		Task ChangeCoupon(OrderCoupon orderCoupon, Coupon coupon);
		
	}
}
