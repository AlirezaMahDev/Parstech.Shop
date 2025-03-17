using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class OrderDetailMapping : Profile
{
    public OrderDetailMapping()
    {
        CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
    }
}