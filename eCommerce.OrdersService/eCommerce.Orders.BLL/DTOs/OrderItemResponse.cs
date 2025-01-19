namespace eCommerce.Orders.BLL.DTOs;

public class OrderItemResponse
{
    public Guid ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public string? Name { get; set; }
    public string? Category { get; set; }
}