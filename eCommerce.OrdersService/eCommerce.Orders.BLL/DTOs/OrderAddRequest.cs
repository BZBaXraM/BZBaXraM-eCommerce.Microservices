namespace eCommerce.Orders.BLL.DTOs;

public record OrderAddRequest(Guid UserId, DateTime OrderDate, List<OrderItemAddRequest> OrderItems)
{
    public OrderAddRequest(): this(default, default, default)
    {
    }
}