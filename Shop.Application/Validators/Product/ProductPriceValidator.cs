using FluentValidation;
using Shop.Application.DTOs.ProductStockPrice;
using Shop.Application.DTOs.WalletTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Validators.Product
{
    
    public class ProductPriceValidator : AbstractValidator<ProductStockPriceDto>
    {
        public ProductPriceValidator()
        {
            
            RuleFor(x => x.DiscountPrice).LessThanOrEqualTo(x => x.SalePrice)
            .WithMessage("قیمت تخفیف نمیتواند بیشتر از قیمت فروش محصول باشد");

            RuleFor(model => model.TextPrice)
            .NotEmpty()
            .WithMessage("مقدار ورودی نمی‌تواند خالی باشد.")
            .Matches("^[0-9,]+$")
            .WithMessage("مقدار ورودی تنها باید شامل اعداد باشد.");
            
            RuleFor(model => model.TextSalePrice)
            .NotEmpty()
            .WithMessage("مقدار ورودی نمی‌تواند خالی باشد.")
            .Matches("^[0-9,]+$")
            .WithMessage("مقدار ورودی تنها باید شامل اعداد باشد.");
            
            RuleFor(model => model.TextDiscountPrice)
            .NotEmpty()
            .WithMessage("مقدار ورودی نمی‌تواند خالی باشد.")
            .Matches("^[0-9,]+$")
            .WithMessage("مقدار ورودی تنها باید شامل اعداد باشد.");
            
            RuleFor(model => model.TextPrice)
            .NotEmpty()
            .WithMessage("مقدار ورودی نمی‌تواند خالی باشد.")
            .Matches("^[0-9,]+$")
            .WithMessage("مقدار ورودی تنها باید شامل اعداد باشد.");
        }

    }
}
