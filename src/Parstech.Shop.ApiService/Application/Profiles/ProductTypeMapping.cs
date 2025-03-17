using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductTypeMapping : Profile
{
    public ProductTypeMapping()
    {
        CreateMap<ProductType, ProductTypeDto>().ReverseMap();
    }
}