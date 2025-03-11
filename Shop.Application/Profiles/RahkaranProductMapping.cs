using AutoMapper;
using Shop.Application.DTOs.Rahkaran;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class RahkaranProductMapping : Profile
    {
        public RahkaranProductMapping()
        {
            CreateMap<RahkaranProduct, RahkaranProductDto>().ReverseMap();
        }
    }
}
