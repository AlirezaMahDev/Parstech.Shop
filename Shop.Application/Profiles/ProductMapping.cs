using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.Section;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductListShowDto>().ReverseMap();
            CreateMap<DapperProductDto, ProductListShowDto>().ReverseMap();
            CreateMap<DapperProductDto, ProductDto>().ReverseMap();
            CreateMap<ProductDto, ProductQuickEditDto>().ReverseMap();
        }
    }
}
