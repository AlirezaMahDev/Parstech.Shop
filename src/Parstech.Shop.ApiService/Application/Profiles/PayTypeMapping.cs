using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class PayTypeMapping : Profile
{
    public PayTypeMapping()
    {
        CreateMap<PayType, PayTypeDto>().ReverseMap();
    }
}