using FluentValidation;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Validators.Coupon;

public class CouponDtoValidator : AbstractValidator<CouponDto>
{
    public CouponDtoValidator()
    {
        RuleFor(z => z.Persent)
            .Must(z => z > 0)
            .WithMessage("مقدار تخفیف نمی تواند صفر یا کمتر باشد");

        RuleFor(z => z.Amount)
            .Must(z => z > 0)
            .WithMessage("مقدار تخفیف نمی تواند صفر یا کمتر باشد");
        RuleFor(z => z.MinPrice)
            .Must(z => z > 0)
            .WithMessage("حداقل قیمت نمی تواند صفر یا کمتر باشد");
        RuleFor(z => z.LimitUse)
            .Must(z => z > 0)
            .WithMessage("تعداد دفعات استفاده نمی تواند صفر یا کمتر باشد");
        RuleFor(z => z.LimitEachUser)
            .Must(z => z > 0)
            .WithMessage("تعداد دفعات استفاده هر کاربر نمی تواند صفر یا کمتر باشد");
        RuleFor(z => new { z.MinPrice, z.MaxPrice })
            .Must(z => z.MaxPrice > z.MinPrice)
            .WithMessage("حداقل قیمت نمی تواند بیشتر یا برابر حداکثر قیمت باشد");
    }
}