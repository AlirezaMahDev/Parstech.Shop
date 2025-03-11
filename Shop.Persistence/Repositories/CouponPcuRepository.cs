using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Application.DTOs.CouponPcu;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
	public class CouponPcuRepository : GenericRepository<CouponPcu>, ICouponPcuRepository
	{
		private readonly DatabaseContext _context;
		public CouponPcuRepository(DatabaseContext context) : base(context)
		{
			_context = context;
		}

		public async Task<List<CouponPcu>> GetPCUOfCoupon(string Type,bool Status, Coupon coupon)
		{
			CouponListPcuDto Result = new CouponListPcuDto();
			Result.list = new List<CouponCheckPcuDto>();
			var list =await _context.CouponPcus.Where(u =>u.CouponId==coupon.Id &&u.Type == Type && u.YesOrNo == Status).ToListAsync();
            
			return list;
		}

		
	}
}
