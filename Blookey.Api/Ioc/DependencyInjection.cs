using Blookey.Application.Common.Behaviors;
using Blookey.Application.Common.Interfaces;
using Blookey.Domain.Interfaces;
using Blookey.Infrastructure.Data.Context;
using Blookey.Infrastructure.Data.Identity.Services;
using Blookey.Infrastructure.Extensions;
using Blookey.Infrastructure.Integrations.Assas.Client;
using Blookey.Infrastructure.Integrations.Assas.Subaccounts;
using Blookey.Infrastructure.Integrations.Email;
using Blookey.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blookey.Api.Ioc;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<BlookeyContext>());
        services.AddDbContext<BlookeyContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(BlookeyContext).Assembly.FullName)
            );
        });

        services.AddIdentityConfiguration(configuration);

        // HTTPClients
        services.AddHttpClient<AssasHttpClient>();
        services.Configure<AssasClientOptions>(configuration.GetSection(AssasClientOptions.Section));


        // Repositories
        services.AddScoped<IAddressRepository, AddressRepository>();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAssasSubaccountService, AssasSubaccountService>();
        services.AddScoped<IEmailService, EmailService>();




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
