using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class PropertyMapping : Profile
{
    public PropertyMapping()
    {
        CreateMap<Property, PropertyDto>().ReverseMap();
    }
}