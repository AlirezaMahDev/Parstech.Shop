using FluentValidation;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Validators.SiteSetting;

public class SiteDtoValidator : AbstractValidator<SiteDto>
{
    public SiteDtoValidator()
    {
    }
}