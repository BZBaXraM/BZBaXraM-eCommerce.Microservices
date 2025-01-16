namespace eCommerce.Orders.DAL.DTOs;

public class OrderItemUpdateRequest
{
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}