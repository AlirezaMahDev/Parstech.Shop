using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.SiteSetting;
using Shop.Application.DTOs.User;
using Shop.Application.DTOs.UserBilling;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class UserBillingMapping:Profile
    {
        public UserBillingMapping()
        {
            CreateMap<UserBilling, UserBillingDto>().ReverseMap();
            CreateMap<UserBilling, UserRegisterDto>().ReverseMap();

            CreateMap<UserBillingDto, UserRegisterDto>().ReverseMap();
            CreateMap<UserDto, UserRegisterDto>().ReverseMap();
        }
    }
}
