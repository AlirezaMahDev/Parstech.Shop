using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class SiteSettingMapping : Profile
{
    public SiteSettingMapping()
    {
        CreateMap<SiteSetting, SiteDto>().ReverseMap();
        CreateMap<SiteSetting, SeoDto>().ReverseMap();
    }
}