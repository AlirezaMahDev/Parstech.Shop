using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class OrderPayMapping : Profile
{
    public OrderPayMapping()
    {
        CreateMap<OrderPay, OrderPayDto>().ReverseMap();
    }
}