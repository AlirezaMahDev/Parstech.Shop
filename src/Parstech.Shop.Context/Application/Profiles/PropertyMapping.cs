using AutoMapper;

using Parstech.Shop.Context.Application.DTOs.Property;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class PropertyMapping:Profile
{
    public PropertyMapping()
    {
        CreateMap<Property, PropertyDto>().ReverseMap();
    }
}