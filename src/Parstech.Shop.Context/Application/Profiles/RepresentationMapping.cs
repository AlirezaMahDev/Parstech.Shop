using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Representation;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class RepresentationMapping:Profile
{
    public RepresentationMapping()
    {
        CreateMap<Representation, RepresentationDto>().ReverseMap();
    }
}