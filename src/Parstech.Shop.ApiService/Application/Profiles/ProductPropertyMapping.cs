using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductPropertyMapping : Profile
{
    public ProductPropertyMapping()
    {
        CreateMap<ProductProperty, ProductPropertyDto>().ReverseMap();
        CreateMap<DapperProductDto, ProductPropertyDto>().ReverseMap();
    }
}