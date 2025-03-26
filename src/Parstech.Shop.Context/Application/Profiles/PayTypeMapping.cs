using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.PayType;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class PayTypeMapping:Profile
{
    public PayTypeMapping()
    {
        CreateMap<PayType,PayTypeDto>().ReverseMap();
    }
}