using eCommerce.Products.BLL.DTOs;
using FluentValidation;

namespace eCommerce.Products.BLL.Validators;

public class ProductAddRequestValidator : AbstractValidator<ProductAddRequest>
{
    public ProductAddRequestValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Category)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0);

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0);
    }
}