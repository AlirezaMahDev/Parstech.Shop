using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductMapping : Profile
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