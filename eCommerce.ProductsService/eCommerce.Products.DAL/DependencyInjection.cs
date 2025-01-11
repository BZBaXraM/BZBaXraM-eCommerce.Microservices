using eCommerce.Products.DAL.Data;
using eCommerce.Products.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Products.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProductContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IProductsRepository, ProductRepository>();
        
        return services;
    }
}