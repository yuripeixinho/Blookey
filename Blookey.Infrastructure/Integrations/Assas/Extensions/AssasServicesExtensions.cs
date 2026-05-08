using Blookey.Application.Common.Interfaces;
using Blookey.Infrastructure.Integrations.Assas.Client;
using Blookey.Infrastructure.Integrations.Assas.Subaccounts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blookey.Infrastructure.Integrations.Assas.Extensions;

public static class AssasServiceExtensions
{
    /// <summary>
    /// Registers the Asaas typed HttpClient with API key injection and retry policy.
    /// Required configuration keys:
    ///   Assas:BaseUrl  — e.g. https://sandbox.asaas.com/api
    ///   Assas:ApiKey   — your master API key ($aact_...)
    /// </summary>
    public static IServiceCollection AddAssasIntegration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var baseUrl = configuration["Assas:BaseUrl"]
            ?? throw new InvalidOperationException("Assas:BaseUrl não configurado.");

        var apiKey = configuration["Assas:AccessToken"]
            ?? throw new InvalidOperationException("Assas:AccessToken não configurado.");

        services.AddHttpClient<AssasHttpClient>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("access_token", apiKey);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "Blookey/1.0");
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddScoped<IAssasSubaccountService, AssasSubaccountService>();

        return services;
    }
}
