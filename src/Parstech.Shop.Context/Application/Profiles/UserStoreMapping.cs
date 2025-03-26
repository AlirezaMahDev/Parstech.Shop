using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.UserStore;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class UserStoreMapping:Profile
{
    public UserStoreMapping()
    {
        CreateMap<UserStore, UserStoreDto>().ReverseMap();
    }
        
}