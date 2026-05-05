namespace Blookey.Domain.Common;

public enum ErrorType
{
    None,
    Failure,        // erro genérico de domínio
    Validation,     // violação de regra de entrada
    NotFound,       // recurso inexistente
    Conflict,       // colisão de estado (ex: email duplicado)
    Unauthorized,   // sem permissão
    Forbidden       // autenticado, mas sem acesso
}