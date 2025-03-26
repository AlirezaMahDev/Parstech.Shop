using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Coupon;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class CouponMapping:Profile
{
    public CouponMapping() 
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}