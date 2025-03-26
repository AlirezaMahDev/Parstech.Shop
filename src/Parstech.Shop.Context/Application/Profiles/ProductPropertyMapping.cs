using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Application.DTOs.ProductProperty;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductPropertyMapping:Profile
{
    public ProductPropertyMapping()
    {
        CreateMap<ProductProperty, ProductPropertyDto>().ReverseMap();
        CreateMap<DapperProductDto, ProductPropertyDto>().ReverseMap();
    }
}