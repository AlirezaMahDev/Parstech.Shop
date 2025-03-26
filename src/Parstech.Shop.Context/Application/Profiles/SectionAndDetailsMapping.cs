using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.Section;
using Parstech.Shop.Context.Application.DTOs.SectionDetail;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class SectionAndDetailsMapping:Profile
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