using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.OrderDetail;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class OrderDetailMapping : Profile
{
    public OrderDetailMapping() 
    {
        CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
    }

}