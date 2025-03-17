using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class CouponMapping : Profile
{
    public CouponMapping()
    {
        CreateMap<Coupon, CouponDto>().ReverseMap();
    }
}