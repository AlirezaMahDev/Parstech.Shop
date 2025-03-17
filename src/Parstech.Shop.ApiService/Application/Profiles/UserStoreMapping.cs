using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class UserStoreMapping : Profile
{
    public UserStoreMapping()
    {
        CreateMap<UserStore, UserStoreDto>().ReverseMap();
    }
}