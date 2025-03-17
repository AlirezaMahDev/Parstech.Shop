using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class PropertyCateguryMapping : Profile
{
    public PropertyCateguryMapping()
    {
        CreateMap<PropertyCategury, PropertyCateguryDto>().ReverseMap();
    }
}