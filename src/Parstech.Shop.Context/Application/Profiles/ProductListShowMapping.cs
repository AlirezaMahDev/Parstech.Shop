using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductListShowMapping : Profile
{
    public ProductListShowMapping()
    {
        CreateMap<Product, ProductListShowDto>().ReverseMap();
    }
}