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
    public class ProductDetailShowMapping : Profile
    {
        public ProductDetailShowMapping()
        {
            CreateMap<Product, ProductDetailShowDto>().ReverseMap();
        }
    }
}
