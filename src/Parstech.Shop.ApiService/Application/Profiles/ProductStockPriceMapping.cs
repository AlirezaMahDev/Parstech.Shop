
using AutoMapper;
using Shop.Application.DTOs.Product;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Profiles
{
    public class ProductStockPriceMapping:Profile
    {
        public ProductStockPriceMapping()
        {
            CreateMap<ProductStockPrice,ProductStockPriceDto>().ReverseMap();
            CreateMap<ProductStockPrice,ProductDto>().ReverseMap();
            CreateMap<ProductStockPrice, ProductListShowDto>().ReverseMap();
        }
    }
}
