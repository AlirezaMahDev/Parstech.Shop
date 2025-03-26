using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.OrderCoupon;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class OrderCouponMapping:Profile
{
    public OrderCouponMapping()
    {
        CreateMap<OrderCoupon,OrderCouponDto>().ReverseMap();
    }
}