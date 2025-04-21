using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.Application.Contracts.Persistance;
using Shop.Domain.Models;
using Shop.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Persistence.Repositories
{
    public class OrderCouponRepository : GenericRepository<OrderCoupon>, IOrderCouponRepository
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public OrderCouponRepository(DatabaseContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CheckAndDelete(int orderId)
        {
            var item=await GetByOrderId(orderId);
            if (item != null)
            {
                await DeleteAsync(item);
            }
        }

        public async Task<bool> CouponExistInOrder(int orderId,int couponId)
        {
            var exist = await _context.OrderCoupons.FirstOrDefaultAsync(a =>a.OrderId==orderId&& a.CouponId== couponId);
            if (exist == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> ExistInOrder(int orderId)
        {
            var exist = await _context.OrderCoupons.FirstOrDefaultAsync(a =>a.OrderId==orderId);
            if (exist == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> CouponExistInOrderCoupon(int couponId)
        {
            var exist = await _context.OrderCoupons.FirstOrDefaultAsync(a =>  a.CouponId == couponId);
            if (exist == null)
            {
                return false;
            }
            return true;
        }

        public async Task<OrderCoupon?> GetByOrderId(int orderId)
		{
			return await _context.OrderCoupons.FirstOrDefaultAsync(u=>u.OrderId==orderId);
		}

		public async Task<bool> OrderHaveCoupon(int orderId)
		{
			var exist = await _context.OrderCoupons.FirstOrDefaultAsync(a => a.OrderId == orderId);
			if (exist == null)
			{
				return false;
			}
			return true;
		}
	}
}
