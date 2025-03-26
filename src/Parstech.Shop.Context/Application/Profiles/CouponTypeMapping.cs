using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.CouponType;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class CouponTypeMapping:Profile
{
    public CouponTypeMapping()
    {
        CreateMap<CouponType, CouponTypeDto>().ReverseMap();
    }
}