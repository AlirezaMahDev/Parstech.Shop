using AutoMapper;

using Parstech.Shop.ApiService.Application.DTOs;
using Parstech.Shop.ApiService.Domain.Models;

namespace Parstech.Shop.ApiService.Application.Profiles;

public class SiteSettingMapping : Profile
{
    public SiteSettingMapping()
    {
        CreateMap<SiteSetting, SiteDto>().ReverseMap();
        CreateMap<SiteSetting, SeoDto>().ReverseMap();
    }
}