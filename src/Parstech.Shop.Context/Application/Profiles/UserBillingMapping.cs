using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Application.DTOs.UserBilling;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class UserBillingMapping:Profile
{
    public UserBillingMapping()
    {
        CreateMap<UserBilling, UserBillingDto>().ReverseMap();
        CreateMap<UserBilling, UserRegisterDto>().ReverseMap();

        CreateMap<UserBillingDto, UserRegisterDto>().ReverseMap();
        CreateMap<UserDto, UserRegisterDto>().ReverseMap();
    }
}