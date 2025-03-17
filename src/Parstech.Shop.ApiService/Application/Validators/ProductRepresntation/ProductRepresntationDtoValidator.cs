using FluentValidation;

using Parstech.Shop.Shared.DTOs;

namespace Parstech.Shop.ApiService.Application.Validators.ProductRepresntation;

public class ProductRepresntationDtoValidator : AbstractValidator<ProductRepresentationDto>
{
    public ProductRepresntationDtoValidator()
    {
        RuleFor(u => u.Quantity)
            .Must(u => u > 0)
            .WithMessage("تعداد موجودی نمی تواند  صفر و کمتر از صفر باشد");
    }
}