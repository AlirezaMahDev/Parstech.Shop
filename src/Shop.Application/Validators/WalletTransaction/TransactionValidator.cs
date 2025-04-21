using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Application.DTOs.Auth;
using Shop.Application.DTOs.WalletTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Validators.WalletTransaction
{
    
    public class TransactionValidator : AbstractValidator<WalletTransactionDto>
    {
        public TransactionValidator()
        {
            RuleFor(model => model.Persent)
                .NotEmpty().WithMessage("درصد سود باید بزرگتر از صفر باشد.")
             .GreaterThanOrEqualTo(0)
             .WithMessage("درصد سود باید بزرگتر از صفر باشد.");
            
            RuleFor(model => model.Month)
                .NotEmpty().WithMessage("درصد سود باید بزرگتر از صفر باشد.")
             .GreaterThan(0)
             .WithMessage("ماه های بازپرداخت باید بزرگتر از صفر باشد.");

            RuleFor(model =>model.InputPrice)
            .NotEmpty()
            .WithMessage("مقدار ورودی نمی‌تواند خالی باشد.")
            .Matches("^[0-9,]+$")
            .WithMessage("مقدار ورودی تنها باید شامل اعداد باشد.");

        }

    }
}
