using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.Brand;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class BrandMapping:Profile
    {
        public BrandMapping()
        {
            CreateMap<Brand, BrandDto>().ReverseMap();
        }
    }
}
