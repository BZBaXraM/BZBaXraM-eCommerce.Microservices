namespace eCommerce.Users.Infrastructure.Repositories;

internal class UsersRepository(UsersContext context) : IUsersRepository
{
    public async Task<AppUser?> AddUserAsync(AppUser user)
    {
        user.UserId = Guid.NewGuid();

        await context.Users.AddAsync(user);

        await context.SaveChangesAsync();

        return user;
    }

    public async Task<AppUser?> GetUserByEmailAndPasswordAsync(string? email, string? password)
    {
        var users = await context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        return users ?? null;
    }

    public async Task<AppUser?> GetUserByUserIdAsync(Guid userId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

        return user ?? null;
    }

    public async Task<bool> DeleteUserAsync(Guid userId)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.UserId == userId);

        if (user is null)
        {
            return false;
        }

        context.Users.Remove(user);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<IReadOnlyList<AppUser>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }
}