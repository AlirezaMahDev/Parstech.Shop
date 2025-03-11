using AutoMapper;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductGallery;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class ProductListShowMapping : Profile
    {
        public ProductListShowMapping()
        {
            CreateMap<Product, ProductListShowDto>().ReverseMap();
        }
    }
}
