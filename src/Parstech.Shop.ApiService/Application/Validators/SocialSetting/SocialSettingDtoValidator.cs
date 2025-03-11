using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Shop.Application.DTOs.SiteSetting;
using Shop.Application.DTOs.SocialSetting;

namespace Shop.Application.Validators.SocialSetting
{
    public class SocialSettingDtoValidator: AbstractValidator<SocialSettingDto>
    {
        public SocialSettingDtoValidator()
        {
            
        }
    }
}
