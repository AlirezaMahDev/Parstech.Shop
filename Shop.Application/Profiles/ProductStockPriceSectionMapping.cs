using AutoMapper;
using Shop.Application.DTOs.Brand;
using Shop.Application.DTOs.ProductStockPriceSection;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class ProductStockPriceSectionMapping:Profile
    {
        public ProductStockPriceSectionMapping()
        {
            CreateMap<ProductStockPriceSection, SectionDto>().ReverseMap();
        }
    }
}
