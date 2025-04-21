using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.Property;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class ProductGalleryMapping:Profile
    {
        public ProductGalleryMapping()
        {
            CreateMap<ProductGallery, ProductGalleryDto>().ReverseMap();
        }
    }
}
