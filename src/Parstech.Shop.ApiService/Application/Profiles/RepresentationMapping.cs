using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class RepresentationMapping : Profile
{
    public RepresentationMapping()
    {
        CreateMap<Representation, RepresentationDto>().ReverseMap();
    }
}