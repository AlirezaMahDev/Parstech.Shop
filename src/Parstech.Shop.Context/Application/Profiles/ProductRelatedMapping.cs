using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.ProductRelated;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class ProductRelatedMapping : Profile
{
    public ProductRelatedMapping()
    {
        CreateMap<ProductRelated, ProductRelatedDto>().ReverseMap();
    }
}