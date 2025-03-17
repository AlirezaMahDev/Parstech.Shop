using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductDetailShowMapping : Profile
{
    public ProductDetailShowMapping()
    {
        CreateMap<Product, ProductDetailShowDto>().ReverseMap();
    }
}