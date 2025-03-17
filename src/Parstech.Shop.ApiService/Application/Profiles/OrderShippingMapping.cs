using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class OrderShippingMapping : Profile
{
    public OrderShippingMapping()
    {
        CreateMap<OrderShipping, OrderShippingDto>().ReverseMap();
    }
}