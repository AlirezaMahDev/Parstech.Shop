using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.IUserRole;
using Shop.Application.DTOs.Section;
using Shop.Application.DTOs.User;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class UserMapping:Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserRegisterDto>().ReverseMap();
            CreateMap<IUserRoleDto, UserRegisterDto>().ReverseMap();
        }
    }
}
