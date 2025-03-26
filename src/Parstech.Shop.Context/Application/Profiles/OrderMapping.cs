using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Order;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class OrderMapping : Profile
{
    public OrderMapping() 
    {
        CreateMap<Order, OrderDto>().ReverseMap();
    }
}