using eCommerce.Users.Core.Enums;

namespace eCommerce.Users.Core.DTOs;

public record RegisterRequest(
    string? Email,
    string? Password,
    string? PersonName,
    GenderOptions Gender);