using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class OrderShippingMapping : Profile
{
    public OrderShippingMapping()
    {
        CreateMap<OrderShipping, OrderShippingDto>().ReverseMap();
    }
}