using Blookey.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Blookey.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException("Cannot convert a success result to ProblemDetails.");

        if (result.Error.Type == ErrorType.Validation
            && result.Error.ValidationErrors is not null)
        {
            var validationProblem = new ValidationProblemDetails(
                result.Error.ValidationErrors.ToDictionary(k => k.Key, v => v.Value))
            {
                Title = "Um ou mais erros de validação ocorreram.",
                Status = StatusCodes.Status422UnprocessableEntity
            };

            return Results.Json(validationProblem,
                statusCode: StatusCodes.Status422UnprocessableEntity,
                contentType: "application/problem+json");
        }

        var problem = new ProblemDetails
        {
            Title = GetTitle(result.Error.Type),
            Detail = result.Error.Description,
            Status = GetStatusCode(result.Error.Type)
        };

        return Results.Json(problem,
            statusCode: problem.Status,
            contentType: "application/problem+json");
    }

    private static int GetStatusCode(ErrorType type) => type switch
    {
        ErrorType.Validation => StatusCodes.Status400BadRequest,
        ErrorType.NotFound => StatusCodes.Status404NotFound,
        ErrorType.Conflict => StatusCodes.Status409Conflict,
        ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
        ErrorType.Forbidden => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status500InternalServerError
    };

    private static string GetTitle(ErrorType type) => type switch
    {
        ErrorType.Validation => "Bad Request",
        ErrorType.NotFound => "Not Found",
        ErrorType.Conflict => "Conflict",
        ErrorType.Unauthorized => "Unauthorized",
        ErrorType.Forbidden => "Forbidden",
        _ => "Internal Server Error"
    };
}