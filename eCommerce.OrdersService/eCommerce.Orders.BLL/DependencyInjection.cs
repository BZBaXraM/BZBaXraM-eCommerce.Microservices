namespace eCommerce.Orders.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBll(this IServiceCollection services)
    {
        services.AddScoped<IOrdersService, OrdersService>();

        services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();
        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        services.AddTransient<IUsersMicroservicePolicies, UsersMicroservicePolicies>();

        return services;
    }
}