using FluentValidation;
using Shop.Application.DTOs.Coupon;
using Shop.Application.DTOs.FormCredit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Validators.Coupon
{
    public class FormCreditDtoValidator : AbstractValidator<FormCreditDto>
    {
        public FormCreditDtoValidator() 
        {
            RuleFor(model => model.Name)
           .NotEmpty()
           .WithMessage("نام نمی‌تواند خالی باشد.");
            
            RuleFor(model => model.Family)
           .NotEmpty()
           .WithMessage("نام حانوادگی نمی‌تواند خالی باشد.");
            
            RuleFor(model => model.PersonalCode)
           .NotEmpty()
           .WithMessage("کد پرسنلی نمی‌تواند خالی باشد.");
            
            RuleFor(model => model.InternationalCode)
           .NotEmpty()
           .WithMessage("کد ملی نمی‌تواند خالی باشد.");
            
            RuleFor(model => model.Mobile)
           .NotEmpty()
           .WithMessage("تلفن همراه نمی‌تواند خالی باشد."); 
            
            RuleFor(model => model.State)
           .NotEmpty()
           .WithMessage("استان نمی‌تواند خالی باشد.");

            RuleFor(model => model.TextRequestPrice)
           .NotEmpty()
           .WithMessage("مبلغ درخواستی نمی‌تواند خالی باشد.");
          

        }
    }
}
