
using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductStockPrice;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductStockPriceMapping:Profile
{
    public ProductStockPriceMapping()
    {
        CreateMap<ProductStockPrice,ProductStockPriceDto>().ReverseMap();
        CreateMap<ProductStockPrice,ProductDto>().ReverseMap();
        CreateMap<ProductStockPrice, ProductListShowDto>().ReverseMap();
    }
}