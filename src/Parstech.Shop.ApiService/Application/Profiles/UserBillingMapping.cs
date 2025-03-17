using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class UserBillingMapping : Profile
{
    public UserBillingMapping()
    {
        CreateMap<UserBilling, UserBillingDto>().ReverseMap();
        CreateMap<UserBilling, UserRegisterDto>().ReverseMap();

        CreateMap<UserBillingDto, UserRegisterDto>().ReverseMap();
        CreateMap<UserDto, UserRegisterDto>().ReverseMap();
    }
}