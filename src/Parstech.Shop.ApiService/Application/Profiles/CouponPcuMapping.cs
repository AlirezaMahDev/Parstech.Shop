using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class CouponPcuMapping : Profile
{
    public CouponPcuMapping()
    {
        CreateMap<CouponPcu, CouponPcuDto>().ReverseMap();
        CreateMap<CouponPcu, CouponCheckPcuDto>().ReverseMap();
    }
}