using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class ProductRelatedMapping : Profile
{
    public ProductRelatedMapping()
    {
        CreateMap<ProductRelated, ProductRelatedDto>().ReverseMap();
    }
}