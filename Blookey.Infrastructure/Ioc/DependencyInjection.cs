using Blookey.Application.Common.Behaviors;
using Blookey.Application.Common.Interfaces;
using Blookey.Infrastructure.Data.Auth.Services;
using Blookey.Infrastructure.Data.Context;
using Blookey.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blookey.Infrastructure.Ioc;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BlookeyContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(BlookeyContext).Assembly.FullName)
            );
        });

        services.AddIdentityConfiguration(configuration);

        // Repositories
        //services.AddScoped<IPlantSpeciesRepository, PlantSpeciesRepository>();

        // Services
        services.AddScoped<IAuthService, AuthService>();

        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("Blookey.Application"));

            // Adiciona o comportamento de validação na pipeline
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });


        return services;
    }
}
