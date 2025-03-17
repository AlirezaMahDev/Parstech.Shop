using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class PropertyCateguryMapping : Profile
{
    public PropertyCateguryMapping()
    {
        CreateMap<PropertyCategury, PropertyCateguryDto>().ReverseMap();
    }
}