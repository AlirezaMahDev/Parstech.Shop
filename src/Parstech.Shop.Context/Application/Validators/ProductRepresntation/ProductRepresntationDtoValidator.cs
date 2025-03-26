using FluentValidation;

using Parstech.Shop.Context.Application.DTOs.ProductRepresentation;

namespace Parstech.Shop.Context.Application.Validators.ProductRepresntation;

public class ProductRepresntationDtoValidator : AbstractValidator<ProductRepresentationDto>
{
    public ProductRepresntationDtoValidator()
    {

        RuleFor(u => u.Quantity)
            .Must(u=>u>0).WithMessage("تعداد موجودی نمی تواند  صفر و کمتر از صفر باشد");

            
    }
}