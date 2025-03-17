using AutoMapper;

using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Persistence.Context;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

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
        OrderCoupon? item = await GetByOrderId(orderId);
        if (item != null)
        {
            await DeleteAsync(item);
        }
    }

    public async Task<bool> CouponExistInOrder(int orderId, int couponId)
    {
        OrderCoupon? exist =
            await _context.OrderCoupons.FirstOrDefaultAsync(a => a.OrderId == orderId && a.CouponId == couponId);
        if (exist == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> ExistInOrder(int orderId)
    {
        OrderCoupon? exist = await _context.OrderCoupons.FirstOrDefaultAsync(a => a.OrderId == orderId);
        if (exist == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> CouponExistInOrderCoupon(int couponId)
    {
        OrderCoupon? exist = await _context.OrderCoupons.FirstOrDefaultAsync(a => a.CouponId == couponId);
        if (exist == null)
        {
            return false;
        }

        return true;
    }

    public async Task<OrderCoupon?> GetByOrderId(int orderId)
    {
        return await _context.OrderCoupons.FirstOrDefaultAsync(u => u.OrderId == orderId);
    }

    public async Task<bool> OrderHaveCoupon(int orderId)
    {
        OrderCoupon? exist = await _context.OrderCoupons.FirstOrDefaultAsync(a => a.OrderId == orderId);
        if (exist == null)
        {
            return false;
        }

        return true;
    }
}