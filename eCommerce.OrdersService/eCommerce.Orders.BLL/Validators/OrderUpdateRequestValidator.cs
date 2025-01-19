namespace eCommerce.Orders.BLL.Validators;

public class OrderUpdateRequestValidator : AbstractValidator<OrderUpdateRequest>
{
    public OrderUpdateRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.OrderDate).NotEmpty();
        RuleFor(x => x.OrderItems).NotEmpty();
        RuleForEach(x => x.OrderItems).SetValidator(new OrderItemUpdateRequestValidator());
    }
}