namespace eCommerce.Users.Core.Services;

public interface IUsersService
{
    Task<AuthResponse?> RegisterAsync(RegisterRequest request);
    Task<AuthResponse?> LoginAsync(LoginRequest request);
    Task<UserDto> GetUserByUserIdAsync(Guid userId);
    Task<bool> DeleteUserAsync(Guid userId);
    Task<IReadOnlyList<AppUser>> GetUsersAsync();
}