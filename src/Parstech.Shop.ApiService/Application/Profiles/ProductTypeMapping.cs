using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductTypeMapping : Profile
{
    public ProductTypeMapping()
    {
        CreateMap<ProductType, ProductTypeDto>().ReverseMap();
    }
}