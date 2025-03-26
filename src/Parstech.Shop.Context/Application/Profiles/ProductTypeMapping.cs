using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.ProductType;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductTypeMapping:Profile
{
    public ProductTypeMapping()
    {
        CreateMap<ProductType, ProductTypeDto>().ReverseMap();
    }
}