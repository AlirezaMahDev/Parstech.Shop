using Shop.Application.DTOs.SiteSetting;
using Shop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.SocialSetting;


namespace Shop.Application.Profiles
{
    public class SocialSettingMapping:Profile
    {
        public SocialSettingMapping()
        {
            CreateMap<SocialSetting, SocialSettingDto>().ReverseMap();
            //CreateMap<List<SocialSetting>, List<SocialSettingDto>>().ReverseMap();
        }
    }
}
