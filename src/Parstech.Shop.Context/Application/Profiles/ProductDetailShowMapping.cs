using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Product;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductDetailShowMapping : Profile
{
    public ProductDetailShowMapping()
    {
        CreateMap<Product, ProductDetailShowDto>().ReverseMap();
    }
}