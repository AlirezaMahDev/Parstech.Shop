using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.IUserRole;
using Parstech.Shop.Context.Application.DTOs.User;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class UserMapping:Profile
{
    public UserMapping()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserRegisterDto>().ReverseMap();
        CreateMap<IUserRoleDto, UserRegisterDto>().ReverseMap();
    }
}