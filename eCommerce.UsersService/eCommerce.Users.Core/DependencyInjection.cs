using eCommerce.Users.Core.Services;
using eCommerce.Users.Core.Validators;
using FluentValidation;

namespace eCommerce.Users.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();

        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

        return services;
    }
}