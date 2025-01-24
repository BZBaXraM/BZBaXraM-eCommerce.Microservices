namespace eCommerce.Users.Infrastructure.Data;

public static class InitData
{
    public static IReadOnlyList<AppUser> Users
        => new List<AppUser>
        {
            new()
            {
                UserId = new Guid("c32f8b42-60e6-4c02-90a7-9143ab37189f"),
                PersonName = "John Doe",
                Email = "test1@example.com",
                Password = "password1",
                Gender = "Male"
            },
            new()
            {
                UserId = new Guid("8ff22c7d-18c7-4ef0-a0ac-988ecb2ac7f5"),
                PersonName = "Jane Smith",
                Email = "test2@example.com",
                Password = "password2",
                Gender = "Female"
            }
        };
}