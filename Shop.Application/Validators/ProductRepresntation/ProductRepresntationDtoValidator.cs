using FluentValidation;
using Shop.Application.DTOs.SiteSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Application.DTOs.ProductGallery;
using Shop.Application.DTOs.ProductRepresentation;

namespace Shop.Application.Validators.ProductRepresntation
{
    public class ProductRepresntationDtoValidator : AbstractValidator<ProductRepresentationDto>
    {
        public ProductRepresntationDtoValidator()
        {

            RuleFor(u => u.Quantity)
                .Must(u=>u>0).WithMessage("تعداد موجودی نمی تواند  صفر و کمتر از صفر باشد");

            
        }
    }
}
