using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.PropertyCategury;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class PropertyCateguryMapping:Profile
{
    public PropertyCateguryMapping()
    {
        CreateMap<PropertyCategury, PropertyCateguryDto>().ReverseMap();
    }
}