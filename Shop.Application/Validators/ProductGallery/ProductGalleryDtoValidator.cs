using FluentValidation;
using Shop.Application.DTOs.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.ProductGallery;

namespace Shop.Application.Validators.ProductGallery
{
    public class ProductGalleryDtoValidator : AbstractValidator<ProductGalleryDto>
    {
        public ProductGalleryDtoValidator()
        {

            RuleFor(u => u.File)
                .NotNull().WithMessage("بارگذاری تصویر محصول الزامی میباشد.");

            RuleFor(u => u.Alt)
                .MaximumLength(100).WithMessage("طول متن جایگزین وارد شده بیش از حد مجاز است");

            
        }
    }
}
