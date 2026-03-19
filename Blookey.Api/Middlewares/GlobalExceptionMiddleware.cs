using Blookey.Application.Common.Exceptions;
using Blookey.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Blookey.Api.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/problem+json";

        object problem = exception switch
        {
            // CASO 1: Validação (Retorna ValidationProblemDetails que suporta a lista 'errors')
            ValidationException validationEx => new ValidationProblemDetails(validationEx.Errors)
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = "Validation Error",
                Detail = "One or more validation errors occurred.",
                Type = "https://httpstatuses.com/422",
            },

            // CASO 2: Regra de Negócio
            DomainException domainException => new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Business Rule Violation",
                Detail = domainException.Message,
                Type = "https://httpstatuses.com/400"
            },

            // CASO 3: Não Encontrado
            NotFoundException notFoundException => new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Not Found",
                Detail = notFoundException.Message,
                Type = "https://httpstatuses.com/404"
            },

            // CASO 4: Não Autorizado
            UnauthorizedException unauthorizedException => new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = unauthorizedException.Message,
                Type = "https://httpstatuses.com/401"
            },

            // CASO 5: Erro genérico não tratado (Default)
            _ => new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred.",
                Type = "https://httpstatuses.com/500"
            }
        };

        context.Response.StatusCode = problem switch
        {
            ProblemDetails p => p.Status ?? StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };

        return context.Response.WriteAsJsonAsync(problem);
    }
}