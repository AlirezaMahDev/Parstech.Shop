using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class UserStoreMapping : Profile
{
    public UserStoreMapping()
    {
        CreateMap<UserStore, UserStoreDto>().ReverseMap();
    }
}