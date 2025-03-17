using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductStockPriceSectionMapping : Profile
{
    public ProductStockPriceSectionMapping()
    {
        CreateMap<ProductStockPriceSection, SectionDto>().ReverseMap();
    }
}