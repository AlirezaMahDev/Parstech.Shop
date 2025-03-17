using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductStockPriceSectionMapping : Profile
{
    public ProductStockPriceSectionMapping()
    {
        CreateMap<ProductStockPriceSection, SectionDto>().ReverseMap();
    }
}