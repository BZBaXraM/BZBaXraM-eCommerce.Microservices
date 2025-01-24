namespace eCommerce.Users.Infrastructure.Data;

public class UsersContext(DbContextOptions<UsersContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
}