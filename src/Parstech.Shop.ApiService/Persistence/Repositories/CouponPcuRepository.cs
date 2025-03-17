using Microsoft.EntityFrameworkCore;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Persistence.Context;

namespace Parstech.Shop.ApiService.Persistence.Repositories;

public class CouponPcuRepository : GenericRepository<CouponPcu>, ICouponPcuRepository
{
    private readonly DatabaseContext _context;

    public CouponPcuRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<CouponPcu>> GetPCUOfCoupon(string Type, bool Status, Coupon coupon)
    {
        CouponListPcuDto Result = new();
        Result.list = new();
        List<CouponPcu> list = await _context.CouponPcus
            .Where(u => u.CouponId == coupon.Id && u.Type == Type && u.YesOrNo == Status)
            .ToListAsync();

        return list;
    }
}