using FluentValidation;

using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Validators.WalletTransaction;

public class TransactionValidator : AbstractValidator<WalletTransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(model => model.Persent)
            .NotEmpty()
            .WithMessage("درصد سود باید بزرگتر از صفر باشد.")
            .GreaterThanOrEqualTo(0)
            .WithMessage("درصد سود باید بزرگتر از صفر باشد.");

        RuleFor(model => model.Month)
            .NotEmpty()
            .WithMessage("درصد سود باید بزرگتر از صفر باشد.")
            .GreaterThan(0)
            .WithMessage("ماه های بازپرداخت باید بزرگتر از صفر باشد.");

        RuleFor(model => model.InputPrice)
            .NotEmpty()
            .WithMessage("مقدار ورودی نمی‌تواند خالی باشد.")
            .Matches("^[0-9,]+$")
            .WithMessage("مقدار ورودی تنها باید شامل اعداد باشد.");
    }
}