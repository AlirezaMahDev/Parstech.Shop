using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductMapping:Profile
{
    public ProductMapping()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, ProductListShowDto>().ReverseMap();
        CreateMap<DapperProductDto, ProductListShowDto>().ReverseMap();
        CreateMap<DapperProductDto, ProductDto>().ReverseMap();
        CreateMap<ProductDto, ProductQuickEditDto>().ReverseMap();
    }
}