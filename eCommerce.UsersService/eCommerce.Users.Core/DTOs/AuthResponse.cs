namespace eCommerce.Users.Core.DTOs;

public record AuthResponse(
    Guid UserId,
    string? Email,
    string? PersonName,
    string? Gender,
    string? Token,
    bool Success)
{
    public AuthResponse() : this(Guid.Empty, null, null, null, null, false)
    {
    }
}