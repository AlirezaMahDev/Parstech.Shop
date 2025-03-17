using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class OrderStatusMapping : Profile
{
    public OrderStatusMapping()
    {
        CreateMap<OrderStatus, OrderStatusDto>().ReverseMap();
    }
}