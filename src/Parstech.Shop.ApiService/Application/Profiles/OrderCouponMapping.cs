using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class OrderCouponMapping : Profile
{
    public OrderCouponMapping()
    {
        CreateMap<OrderCoupon, OrderCouponDto>().ReverseMap();
    }
}