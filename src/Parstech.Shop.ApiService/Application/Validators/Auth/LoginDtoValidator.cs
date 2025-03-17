using FluentValidation;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Validators.Auth;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(u => u.UserName)
            .NotEmpty()
            .WithMessage("لطفا نام کاربری را وارد نمایید")
            .MaximumLength(50)
            .WithMessage("طول نام کاربری وترد شده بیش از حد مجاز است.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("لطفا کلمه عبور خود را وارد نمایید")
            .MaximumLength(50)
            .WithMessage("طول کلمه عبور وارد شده بیش از حد مجاز است");
    }
}

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        CascadeMode = CascadeMode.StopOnFirstFailure;


        RuleFor(u => u.old)
            .NotEmpty()
            .WithMessage("لطفا کلمه عبور را وارد نمایید");

        RuleFor(u => u.newPassword)
            .NotEmpty()
            .WithMessage("لطفا کلمه عبور جدید  خود را وارد نمایید");

        RuleFor(u => u.renewPassword)
            .NotEmpty()
            .WithMessage("لطفا تکرار کلمه عبور جدید خود را وارد نمایید");
        RuleFor(x => x)
            .Custom((x, context) =>
            {
                if (x.newPassword != x.renewPassword)
                {
                    context.AddFailure(nameof(x.renewPassword), "کلمات عبور وارد شده یکسان نمی باشد");
                }
            });
    }
}