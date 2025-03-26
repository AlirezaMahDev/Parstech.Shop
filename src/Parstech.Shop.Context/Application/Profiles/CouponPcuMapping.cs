using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.CouponPcu;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class CouponPcuMapping:Profile
{
    public CouponPcuMapping()
    {
        CreateMap<CouponPcu, CouponPcuDto>().ReverseMap();
        CreateMap<CouponPcu, CouponCheckPcuDto>().ReverseMap();
    }
}