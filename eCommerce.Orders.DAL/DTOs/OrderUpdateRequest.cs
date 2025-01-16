namespace eCommerce.Orders.DAL.DTOs;

public class OrderUpdateRequest
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemUpdateRequest> OrderItems { get; set; } = [];
}