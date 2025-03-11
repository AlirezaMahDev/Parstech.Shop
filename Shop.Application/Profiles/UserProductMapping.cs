using AutoMapper;
using Shop.Application.DTOs.UserProduct;
using Shop.Application.DTOs.UserShipping;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class UserProductMapping : Profile
    {
        public UserProductMapping()
        {
            CreateMap<UserProduct, UserProductDto>().ReverseMap();
        }
    }
}
