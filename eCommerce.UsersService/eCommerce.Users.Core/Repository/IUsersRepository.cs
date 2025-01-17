namespace eCommerce.Users.Core.Repository;

public interface IUsersRepository
{
    Task<AppUser?> AddUserAsync(AppUser user);
    Task<AppUser?> GetUserByEmailAndPasswordAsync(string? email, string? password);
    Task<AppUser?> GetUserByUserIdAsync(Guid userId);
}