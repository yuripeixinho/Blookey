using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Blookey.Application.Common.Exceptions;

public class ValidationException : ApplicationExceptionBase
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(IEnumerable<ValidationFailure> failures) : base("One or more validation failures have occurred.")
    {
        Errors = failures
           .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
           .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}


public class IdentityValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public IdentityValidationException(IEnumerable<IdentityError> failures) 
    {
        Errors = failures
           .GroupBy(e => e.Code, e => e.Description)
           .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}

