using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductProperty;
using Shop.Application.DTOs.Property;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class ProductPropertyMapping:Profile
    {
        public ProductPropertyMapping()
        {
            CreateMap<ProductProperty, ProductPropertyDto>().ReverseMap();
            CreateMap<DapperProductDto, ProductPropertyDto>().ReverseMap();
        }
    }
}
