using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class UserProductMapping : Profile
{
    public UserProductMapping()
    {
        CreateMap<UserProduct, UserProductDto>().ReverseMap();
    }
}