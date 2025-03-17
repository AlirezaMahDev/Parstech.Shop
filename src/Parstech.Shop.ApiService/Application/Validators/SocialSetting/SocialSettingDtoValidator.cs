using FluentValidation;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Validators.SocialSetting;

public class SocialSettingDtoValidator : AbstractValidator<SocialSettingDto>
{
    public SocialSettingDtoValidator()
    {
    }
}