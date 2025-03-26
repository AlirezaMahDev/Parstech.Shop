using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.OrderShipping;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class OrderShippingMapping:Profile
{
    public OrderShippingMapping()
    {
        CreateMap<OrderShipping, OrderShippingDto>().ReverseMap();
    }
}