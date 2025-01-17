using eCommerce.Orders.BLL.DTOs;
using FluentValidation;

namespace eCommerce.Orders.BLL.Validators;

public class OrderAddRequestValidator : AbstractValidator<OrderAddRequest>
{
    public OrderAddRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.OrderDate).NotEmpty();
        RuleFor(x => x.OrderItems).NotEmpty();
        RuleForEach(x => x.OrderItems).SetValidator(new OrderItemAddRequestValidator());
    }
}