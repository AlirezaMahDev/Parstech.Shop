using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.OrderStatus;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class OrderStatusMapping : Profile
{
    public OrderStatusMapping()
    {
        CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
    }
}