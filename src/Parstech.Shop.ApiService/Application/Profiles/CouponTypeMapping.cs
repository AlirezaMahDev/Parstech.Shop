using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class CouponTypeMapping : Profile
{
    public CouponTypeMapping()
    {
        CreateMap<CouponType, CouponTypeDto>().ReverseMap();
    }
}