using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.ProductStockPriceSection;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductStockPriceSectionMapping:Profile
{
    public ProductStockPriceSectionMapping()
    {
        CreateMap<ProductStockPriceSection, SectionDto>().ReverseMap();
    }
}