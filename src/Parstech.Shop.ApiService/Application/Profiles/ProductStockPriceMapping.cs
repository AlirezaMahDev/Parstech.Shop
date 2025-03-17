using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductStockPriceMapping : Profile
{
    public ProductStockPriceMapping()
    {
        CreateMap<ProductStockPrice, ProductStockPriceDto>().ReverseMap();
        CreateMap<ProductStockPrice, ProductDto>().ReverseMap();
        CreateMap<ProductStockPrice, ProductListShowDto>().ReverseMap();
    }
}