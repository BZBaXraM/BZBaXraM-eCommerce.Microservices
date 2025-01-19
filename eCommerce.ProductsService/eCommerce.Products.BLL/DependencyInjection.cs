namespace eCommerce.Products.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBll(this IServiceCollection services)
    {
        services.AddScoped<IProductsService, ProductsService>();

        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ProductUpdateRequestValidator>();

        return services;
    }
}