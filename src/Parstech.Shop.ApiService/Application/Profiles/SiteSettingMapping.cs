using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Shop.Application.DTOs.SiteSetting;
using Shop.Domain.Models;

namespace Shop.Application.Profiles
{
    public class SiteSettingMapping:Profile
    {
        public SiteSettingMapping()
        {
            CreateMap<SiteSetting, SiteDto>().ReverseMap();
            CreateMap<SiteSetting, SeoDto>().ReverseMap();
        }
    }
}
