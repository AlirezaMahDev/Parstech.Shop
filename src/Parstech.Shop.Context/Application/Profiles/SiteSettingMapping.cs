using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.SiteSetting;
using Parstech.Shop.Context.Domain.Models;

namespace Parstech.Shop.Context.Application.Profiles;

public class SiteSettingMapping:Profile
{
    public SiteSettingMapping()
    {
        CreateMap<SiteSetting, SiteDto>().ReverseMap();
        CreateMap<SiteSetting, SeoDto>().ReverseMap();
    }
}