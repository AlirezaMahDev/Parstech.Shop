using AutoMapper;

using Parstech.Shop.Shared.DTOs;
using Parstech.Shop.Shared.Models;


namespace Parstech.Shop.ApiService.Application.Profiles;

public class SocialSettingMapping : Profile
{
    public SocialSettingMapping()
    {
        CreateMap<SocialSetting, SocialSettingDto>().ReverseMap();
        //CreateMap<List<SocialSetting>, List<SocialSettingDto>>().ReverseMap();
    }
}