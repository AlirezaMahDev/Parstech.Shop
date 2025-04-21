using Shop.Application.DTOs.UserBilling;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.UserShipping;

namespace Shop.Application.Profiles
{
    public class UserShippingMapping:Profile
    {
        public UserShippingMapping()
        {
            CreateMap<UserShipping, UserShippingDto>().ReverseMap();
        }
    }
}
