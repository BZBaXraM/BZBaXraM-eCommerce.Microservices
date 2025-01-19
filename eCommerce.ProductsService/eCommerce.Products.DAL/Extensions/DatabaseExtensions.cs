namespace eCommerce.Products.DAL.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductContext>();

            Console.WriteLine("Starting database migration...");
            await context.Database.MigrateAsync();
            Console.WriteLine("Database migration completed.");

            await SeedAsync(context);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during database initialization: {ex.Message}");
            throw;
        }
    }


    private static async Task SeedAsync(ProductContext context)
    {
        await SeedProductAsync(context);
    }

    private static async Task SeedProductAsync(ProductContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            await context.Products.AddRangeAsync(InitData.Products);
            await context.SaveChangesAsync();
        }
    }
}