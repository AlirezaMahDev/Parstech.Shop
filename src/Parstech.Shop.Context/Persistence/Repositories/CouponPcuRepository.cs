using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Application.DTOs.CouponPcu;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class CouponPcuRepository : GenericRepository<CouponPcu>, ICouponPcuRepository
{
    private readonly DatabaseContext _context;
    public CouponPcuRepository(DatabaseContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<CouponPcu>> GetPCUOfCoupon(string Type,bool Status, Coupon coupon)
    {
        CouponListPcuDto Result = new();
        Result.list = new();
        var list =await _context.CouponPcus.Where(u =>u.CouponId==coupon.Id &&u.Type == Type && u.YesOrNo == Status).ToListAsync();
            
        return list;
    }

		
}