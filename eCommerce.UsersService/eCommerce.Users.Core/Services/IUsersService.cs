using eCommerce.Users.Core.DTOs;

namespace eCommerce.Users.Core.Services;

public interface IUsersService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    Task<AuthResponse?> LoginAsync(LoginRequest request);
}