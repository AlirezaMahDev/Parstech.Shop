using Parstech.Shop.Context.Domain.Models;

using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.UserShipping;

namespace Parstech.Shop.Context.Application.Profiles;

public class UserShippingMapping:Profile
{
    public UserShippingMapping()
    {
        CreateMap<UserShipping, UserShippingDto>().ReverseMap();
    }
}