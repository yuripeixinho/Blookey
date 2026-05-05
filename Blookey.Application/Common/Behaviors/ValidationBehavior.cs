using Blookey.Domain.Common;
using FluentValidation;
using MediatR;

public sealed class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        => _validators = validators;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        // Execução assíncrona para suportar validações que acessam DB/Serviços externos
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, ct)));

        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .ToList();

        if (failures.Count == 0)
            return await next();

        var validationErrors = failures
            .GroupBy(f => f.PropertyName)
            .ToDictionary(
                group => group.Key,
                group => group
                    .Select(f => f.ErrorMessage)
                    .ToArray()
            );

        var error = Error.Validation(validationErrors);

        return CreateFailureResult<TResponse>(error);
    }

    private static TResponse CreateFailureResult<TResponse>(Error error)
        where TResponse : Result
    {
        if (typeof(TResponse) == typeof(Result))
            return (TResponse)(object)Result.Failure(error);

        // Otimização: Cachear o método de reflexão ou usar dynamic pode ajudar na performance,
        // mas para uma API padrão, sua lógica de reflexão está correta.
        var valueType = typeof(TResponse).GenericTypeArguments[0];
        var failureMethod = typeof(Result)
            .GetMethods()
            .First(m => m.Name == nameof(Result.Failure) && m.IsGenericMethod)
            .MakeGenericMethod(valueType);

        return (TResponse)failureMethod.Invoke(null, new object[] { error })!;
    }
}