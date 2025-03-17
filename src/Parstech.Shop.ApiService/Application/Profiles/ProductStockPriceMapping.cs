using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

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