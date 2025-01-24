namespace eCommerce.Orders.BLL.DTOs;

public class OrderResponse
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalBill { get; set; }
    public List<OrderItemResponse> OrderItems { get; set; } = [];
    public string? Email { get; set; }
    public string? PersonName { get; set; }
}