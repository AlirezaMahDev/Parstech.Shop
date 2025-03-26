using Microsoft.EntityFrameworkCore;
using Parstech.Shop.Context.Application.Calculator;
using Parstech.Shop.Context.Application.Contracts.Persistance;
using Parstech.Shop.Context.Domain.Models;
using Parstech.Shop.Context.Persistence.Context;
using Parstech.Shop.Context.Application.DTOs.Order;

namespace Parstech.Shop.Context.Persistence.Repositories;

public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
{
    private readonly DatabaseContext _context;
    public CouponRepository(DatabaseContext context) : base(context)
    {
        _context = context;

    }

    public OrderResponse CheckExpireDate(Coupon coupon)
    {
        OrderResponse Response = new();
        var datetimeNow = DateTime.Now;
        if (coupon.ExpireDate > datetimeNow)
        {
            Response.Status = true;

        }
        else
        {
            Response.Status = false;
            Response.Message = "تاریخ انقضای کوپن وارد شده گذشته است. ";
            Response.Discount = 0;
        }
        return Response;
    }

    public OrderResponse CheckLimitUse(Coupon coupon)
    {
        OrderResponse Response = new();
        if (coupon.LimitUse > 0)
        {
            Response.Status = true;
        }
        else
        {
            Response.Status = false;
            Response.Message = "تعداد استفاده از این کد تخفیف یسشتر از حد مجاز است";
            Response.Discount = 0;
        }
        return Response;
    }

    public OrderResponse CheckMinPrice(long Price, Coupon coupon)
    {
        OrderResponse Response = new();

        if (coupon.MinPrice < Price)
        {
            Response.Status = true;
        }
        else
        {
            Response.Status = false;
            Response.Message = "کمترین مبلغ مجاز جهت استفاده از کد تخفیف  نقض شده است";
            Response.Discount = 0;
        }
        return Response;
    }

    public OrderResponse CheckMaxPrice(long Price, Coupon coupon)
    {
        OrderResponse Response = new();

        switch (coupon.CouponTypeId)
        {
            case 1:
                Response.Status = true;
                break;
            case 2:
                Response.Status = true;
                break;
            default:
                if (coupon.MaxPrice > Price)
                {
                    Response.Status = true;
                }
                else
                {
                    Response.Status = false;
                    Response.Message = "حداکثر مبلغ مجاز جهت استفاده از کد تخفیف  نقض شده است";
                    Response.Discount = 0;
                }
                break;
        }

			
        return Response;
    }

    public long CalculateDiscount(Coupon coupon,long Price)
    {
        long Result = 0;
        switch (coupon.CouponTypeId)
        {
            case 1:
                Result = coupon.Amount;
                break;
            case 2:
                Result = PersentCalculator.PersentCalculatorByPrice(Price, coupon.Persent);
                break;
            case 3:
                Result = coupon.Amount;
                    
                break;
            case 4:
                Result = PersentCalculator.PersentCalculatorByPrice(Price, coupon.Persent);
                break;
            default:
                break;
        }
        return Result;
    }

    public async Task ChangeCoupon(OrderCoupon orderCoupon,Coupon coupon) 
    {
        coupon.LimitUse--;
        switch (coupon.CouponTypeId)
        {
            case 3:
                coupon.Amount -= orderCoupon.DiscountPrice;
                break;
            case 4:
                coupon.Amount -= orderCoupon.DiscountPrice;
                break;
            default:
                break;
        }
        await UpdateAsync(coupon);
			
    }

    public async Task<Coupon?> GetByCouponCode(string couponCode)
    {
        return await _context.Coupons.FirstOrDefaultAsync(u=>u.Code== couponCode);
    }
}