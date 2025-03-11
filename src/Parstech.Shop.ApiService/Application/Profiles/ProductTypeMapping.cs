using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.ProductType;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class ProductTypeMapping:Profile
    {
        public ProductTypeMapping()
        {
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();
        }
    }
}
