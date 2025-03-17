using FluentValidation;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Validators.ProductGallery;

public class ProductGalleryDtoValidator : AbstractValidator<ProductGalleryDto>
{
    public ProductGalleryDtoValidator()
    {
        RuleFor(u => u.File)
            .NotNull()
            .WithMessage("بارگذاری تصویر محصول الزامی میباشد.");

        RuleFor(u => u.Alt)
            .MaximumLength(100)
            .WithMessage("طول متن جایگزین وارد شده بیش از حد مجاز است");
    }
}