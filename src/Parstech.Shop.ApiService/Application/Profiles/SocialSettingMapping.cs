using AutoMapper;

using Parstech.Shop.ApiService.Domain.Models;
using Parstech.Shop.ApiService.Application.DTOs;


namespace Parstech.Shop.ApiService.Application.Profiles;

public class SocialSettingMapping : Profile
{
    public SocialSettingMapping()
    {
        CreateMap<SocialSetting, SocialSettingDto>().ReverseMap();
        //CreateMap<List<SocialSetting>, List<SocialSettingDto>>().ReverseMap();
    }
}