namespace eCommerce.Users.Infrastructure.Data;

public class UsersContext(DbContextOptions<UsersContext> options) : DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PersonName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(15);
        });
    }
}