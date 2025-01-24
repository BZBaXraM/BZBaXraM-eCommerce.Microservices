namespace eCommerce.Orders.BLL.DTOs;

public record OrderItemAddRequest(Guid ProductId, decimal UnitPrice, int Quantity)
{
    public OrderItemAddRequest() : this(default, default, default)
    {
    }
}