using Blookey.Api.Extensions;
using Blookey.Application.Common.Interfaces;
using Blookey.Application.Interfaces;
using Blookey.Application.Services;
using Blookey.Domain.Interfaces;
using Blookey.Domain.Services;
using Blookey.Infrastructure.Data.Context;
using Blookey.Infrastructure.Data.Identity.Services;
using Blookey.Infrastructure.Integrations.Email;
using Blookey.Infrastructure.Repositories;
using Blookey.Infrastructure.Security;
using Microsoft.AspNetCore.DataProtection;
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

        services.AddDataProtection()
        .PersistKeysToFileSystem(new DirectoryInfo("/keys")) // ou Azure Blob, AWS, etc.
        .SetApplicationName("Blookey");

        // Configuração de autenticação e autorização  
        services.AddIdentityConfiguration(configuration);

        // Security
        services.AddScoped<IAsaasKeyProtector, AsaasKeyProtector>();

        // HTTPClients
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, CurrentUserService>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAddressRepository, AddressRepository>();
        services.AddScoped<IPhoneRepository, PhoneRepository>();

        // Services
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();

        // Services de domínio
        services.AddScoped<PhoneDomainService>();

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
