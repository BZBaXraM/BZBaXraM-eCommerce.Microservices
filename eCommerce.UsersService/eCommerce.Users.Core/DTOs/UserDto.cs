namespace eCommerce.Users.Core.DTOs;

public class UserDto // original dto from users service
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? PersonName { get; set; }
    public string? Gender { get; set; }
}