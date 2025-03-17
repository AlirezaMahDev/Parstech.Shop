using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class SectionAndDetailsMapping : Profile
{
    public SectionAndDetailsMapping()
    {
        CreateMap<Section, SectionAndDetailsDto>().ReverseMap();
        CreateMap<SectionDetail, SectionAndDetailsDto>().ReverseMap();

        CreateMap<Section, SectionDto>().ReverseMap();
        CreateMap<SectionDetail, SectionDetailDto>().ReverseMap();
        CreateMap<SectionDetail, SectionDetailShowDto>().ReverseMap();
    }
}