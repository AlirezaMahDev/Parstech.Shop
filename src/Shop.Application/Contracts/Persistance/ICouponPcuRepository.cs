using Shop.Application.DTOs.CouponPcu;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Contracts.Persistance
{
	public interface ICouponPcuRepository:IGenericRepository<CouponPcu>
	{
		Task<List<CouponPcu>> GetPCUOfCoupon(string Type, bool Status, Coupon coupon);
		
	}
}
