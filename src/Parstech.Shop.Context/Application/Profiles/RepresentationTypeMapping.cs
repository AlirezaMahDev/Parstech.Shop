using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.RepresentationType;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class RepresentationTypeMapping:Profile

{
    public RepresentationTypeMapping()
    {
        CreateMap<RepresentationTypeDto, RepresentationType>().ReverseMap();
    }
}