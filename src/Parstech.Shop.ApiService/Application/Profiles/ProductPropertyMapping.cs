using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductPropertyMapping : Profile
{
    public ProductPropertyMapping()
    {
        CreateMap<ProductProperty, ProductPropertyDto>().ReverseMap();
        CreateMap<DapperProductDto, ProductPropertyDto>().ReverseMap();
    }
}