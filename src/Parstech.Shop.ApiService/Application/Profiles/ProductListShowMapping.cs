using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductListShowMapping : Profile
{
    public ProductListShowMapping()
    {
        CreateMap<Product, ProductListShowDto>().ReverseMap();
    }
}