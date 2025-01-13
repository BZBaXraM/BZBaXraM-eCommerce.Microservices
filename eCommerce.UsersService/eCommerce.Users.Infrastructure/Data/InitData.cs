namespace eCommerce.Users.Infrastructure.Data;

public static class InitData
{
    public static IReadOnlyList<AppUser> Users
        => new List<AppUser>
        {
            new()
            {
                UserId = Guid.NewGuid(),
                PersonName = "test",
                Email = "test@test.com",
                Password = "test123",
                Gender = "Male"
            },
        };
}