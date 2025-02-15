namespace eCommerce.Orders.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBll(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrdersService, OrdersService>();

        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        services.AddScoped<IUsersMicroservicePolicies, UsersMicroservicePolicies>();
        services.AddScoped<IProductsMicroservicePolicies, ProductsMicroservicePolicies>();
        services.AddScoped<IPollyPolicies, PollyPolicies>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });

        return services;
    }
}