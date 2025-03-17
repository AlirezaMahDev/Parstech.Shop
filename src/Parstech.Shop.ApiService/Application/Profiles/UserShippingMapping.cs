using AutoMapper;

using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class UserShippingMapping : Profile
{
    public UserShippingMapping()
    {
        CreateMap<UserShipping, UserShippingDto>().ReverseMap();
    }
}