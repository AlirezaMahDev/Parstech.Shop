using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class UserShippingMapping : Profile
{
    public UserShippingMapping()
    {
        CreateMap<UserShipping, UserShippingDto>().ReverseMap();
    }
}