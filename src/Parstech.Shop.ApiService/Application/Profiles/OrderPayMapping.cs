using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class OrderPayMapping : Profile
{
    public OrderPayMapping()
    {
        CreateMap<OrderPay, OrderPayDto>().ReverseMap();
    }
}