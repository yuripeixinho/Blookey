using Blookey.Application.Features.Identity.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Blookey.Api.Extensions;

public static class FluentValidationExtensions
{
    public static void AddFluentValidationConfiguration(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
    }
}
