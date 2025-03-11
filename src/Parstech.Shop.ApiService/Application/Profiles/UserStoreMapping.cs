using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserStore;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class UserStoreMapping:Profile
    {
        public UserStoreMapping()
        {
            CreateMap<UserStore, UserStoreDto>().ReverseMap();
        }
        
    }
}
