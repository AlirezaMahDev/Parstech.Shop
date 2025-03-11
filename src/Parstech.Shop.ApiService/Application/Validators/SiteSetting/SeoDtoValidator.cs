using FluentValidation;
using Shop.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.SiteSetting;

namespace Shop.Application.Validators.SiteSetting
{
    public class SeoDtoValidator : AbstractValidator<SeoDto>
    {
        public SeoDtoValidator()
        {

            RuleFor(u => u.Keywords)
                .MaximumLength(65).WithMessage("طول نام کاربری وترد شده بیش از حد مجاز است.");

            RuleFor(u => u.Description)
                .MaximumLength(160).WithMessage("طول کلمه عبور وارد شده بیش از حد مجاز است");
            
            RuleFor(u => u.OgDescription)
                .MaximumLength(160).WithMessage("طول کلمه عبور وارد شده بیش از حد مجاز است"); 
            
        }
    }
}
