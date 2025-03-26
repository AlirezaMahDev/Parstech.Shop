using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Brand;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class BrandMapping:Profile
{
    public BrandMapping()
    {
        CreateMap<Brand, BrandDto>().ReverseMap();
    }
}