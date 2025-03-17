using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class PayTypeMapping : Profile
{
    public PayTypeMapping()
    {
        CreateMap<PayType, PayTypeDto>().ReverseMap();
    }
}