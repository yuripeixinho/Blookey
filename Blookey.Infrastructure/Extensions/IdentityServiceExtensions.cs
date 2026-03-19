using Blookey.Domain.Identity;
using Blookey.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blookey.Infrastructure.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // 2. Configurar o Identity
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<BlookeyContext>()
            .AddDefaultTokenProviders(); // Necessário para "Esqueci minha senha", etc.

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = false; // Ex: não obrigar @!#
            options.User.RequireUniqueEmail = true;
        });

        return services;
    }
}