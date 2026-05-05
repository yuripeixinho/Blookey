namespace Blookey.Domain.Common;

public sealed record class Error(
    string Code, 
    string Description, 
    ErrorType Type = ErrorType.Failure,
    Dictionary<string, string[]>? ValidationErrors = null)
{
   public static readonly Error None = new(string.Empty, string.Empty, ErrorType.None);

   // Factory methods semânticos
   public static Error NotFound(string resource, object id) =>
        new($"{resource}.NotFound",
            $"{resource} com id '{id}' não foi encontrado.",
            ErrorType.NotFound);

    public static Error Conflict(string code, string description) =>
     new(code, description, ErrorType.Conflict);


    // Sobrecarga original — erro de campo único ainda funciona
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    // Nova sobrecarga — múltiplos campos com múltiplas mensagens cada
    public static Error Validation(Dictionary<string, string[]> errors) =>
        new("Validation.Multiple",
            "Um ou mais erros de validação ocorreram.",
            ErrorType.Validation,
            errors);

    public static Error Unauthorized(string code, string description) =>
        new(code, description, ErrorType.Unauthorized);

    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

}
