using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductListShowMapping : Profile
{
    public ProductListShowMapping()
    {
        CreateMap<Product, ProductListShowDto>().ReverseMap();
    }
}