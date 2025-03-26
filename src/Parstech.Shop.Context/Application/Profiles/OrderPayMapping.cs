using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.OrderPay;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class OrderPayMapping:Profile
{
    public OrderPayMapping()
    {
        CreateMap<OrderPay,OrderPayDto>().ReverseMap();
    }
}