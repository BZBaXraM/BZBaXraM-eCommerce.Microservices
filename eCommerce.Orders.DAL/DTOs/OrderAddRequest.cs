namespace eCommerce.Orders.DAL.DTOs;

public class OrderAddRequest
{
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemAddRequest> OrderItems { get; set; } = [];
}