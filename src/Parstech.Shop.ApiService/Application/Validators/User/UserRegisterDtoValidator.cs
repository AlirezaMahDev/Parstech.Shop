using FluentValidation;

using Parstech.Shop.ApiService.Application.Contracts.Persistance;
using Parstech.Shop.ApiService.Application.DTOs;

namespace Parstech.Shop.ApiService.Application.Validators.User;

public class UserRegisterDtoValidator : AbstractValidator<UserRegisterDto>
{
    private IUserRepository _userRep;

    public UserRegisterDtoValidator(IUserRepository userRep)
    {
        _userRep = userRep;

        RuleFor(u => u.UserName)
            .NotEmpty()
            .WithMessage("نام کاربری نمی تواند خالی باشد");


        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage("نام نمی تواند خالی باشد");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage("نام خانوادگی نمی تواند خالی باشد");


        RuleFor(u => u.Password)
            .MinimumLength(8)
            .WithMessage("طول کلمه عبور کمتر از حد مجاز است")
            .MaximumLength(50)
            .WithMessage("طول کلمه غبور بیشتر از حد مجاز است")
            .NotEmpty()
            .WithMessage("Please confirm your password.");

        RuleFor(u => u.RePassword)
            .MinimumLength(8)
            .WithMessage("طول کلمه عبور کمتر از حد مجاز است")
            .MaximumLength(50)
            .WithMessage("طول کلمه غبور بیشتر از حد مجاز است")
            .NotEmpty()
            .WithMessage("Please confirm your password.")
            .Must((model, Repassword) => _userRep.PaswordsValid(model.Password, Repassword))
            .WithMessage("کلمه های عبور وارد شده با یکدیگر همخوانی ندارند");


        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("آدرس پست الکترونیکی به درستی وارد نشده است");
    }
}

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(u => u.CaptchaValue)
            .NotEmpty()
            .WithMessage("کد امنیتی را وارد نکرده اید");

        RuleFor(u => u.Mobile)
            .NotEmpty()
            .WithMessage("شماره همراه را وارد نکرده اید")
            .MinimumLength(11)
            .WithMessage("شماره همراه به دزستی وارد نشده است")
            .MaximumLength(11)
            .WithMessage("شماره همراه به دزستی وارد نشده است");

        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("نام نمی تواند خالی باشد");

        RuleFor(u => u.Family)
            .NotEmpty()
            .WithMessage("نام خانوادگی نمی تواند خالی باشد");

        RuleFor(u => u.MeliCode)
            .NotEmpty()
            .WithMessage("کد ملی نمی تواند خالی باشد")
            .MinimumLength(10)
            .WithMessage("کد ملی به دزستی وارد نشده است")
            .MaximumLength(10)
            .WithMessage("کد ملی به دزستی وارد نشده است");

        RuleFor(u => u.State)
            .NotEmpty()
            .WithMessage("استان محل سکونت نمی تواند خالی باشد");

        RuleFor(u => u.City)
            .NotEmpty()
            .WithMessage("نشهر محل سکونت نمی تواند خالی باشد");

        RuleFor(u => u.Address)
            .NotEmpty()
            .WithMessage("آدرس محل سکونت نمی تواند خالی باشد");
    }
}