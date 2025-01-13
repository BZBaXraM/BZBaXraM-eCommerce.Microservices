using Microsoft.AspNetCore.Builder;

namespace eCommerce.Users.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<UsersContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(UsersContext context)
    {
        await SeedProductAsync(context);
    }

    private static async Task SeedProductAsync(UsersContext context)
    {
        if (!await context.Users.AnyAsync())
        {
            await context.Users.AddRangeAsync(InitData.Users);
            await context.SaveChangesAsync();
        }
    }
}