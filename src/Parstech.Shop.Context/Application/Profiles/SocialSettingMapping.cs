using Parstech.Shop.Context.Domain.Models;

using AutoMapper;
using Parstech.Shop.Context.Application.DTOs.SocialSetting;


namespace Parstech.Shop.Context.Application.Profiles;

public class SocialSettingMapping:Profile
{
    public SocialSettingMapping()
    {
        CreateMap<SocialSetting, SocialSettingDto>().ReverseMap();
        //CreateMap<List<SocialSetting>, List<SocialSettingDto>>().ReverseMap();
    }
}