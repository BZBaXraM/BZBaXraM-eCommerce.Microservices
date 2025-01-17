namespace eCommerce.Orders.BLL.DTOs;

public class UserDto
{
    public Guid? UserId { get; set; }
    public string? Email { get; set; }
    public string? PersonName { get; set; }
    public string? Gender { get; set; }
}