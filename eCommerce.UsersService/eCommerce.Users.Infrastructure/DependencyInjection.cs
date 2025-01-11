using eCommerce.Users.Infrastructure.Data;
using eCommerce.Users.Infrastructure.Repositories;

namespace eCommerce.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: Add infrastructure services here

        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddDbContext<UsersContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        return services;
    }
}