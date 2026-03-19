namespace Blookey.Application.Common.Exceptions;

// 1. Herda de Exception (do .NET)
// 2. É abstract (não pode ser instanciada diretamente)
public abstract class ApplicationExceptionBase : Exception
{
    // Construtor simples que repassa a mensagem para a classe pai (Exception)
    protected ApplicationExceptionBase(string message)
        : base(message)
    {
    }

    // Opcional: Construtor para repassar a mensagem e uma exceção interna (inner exception)
    // Útil quando você captura um erro de banco e o encapsula num erro de aplicação
    protected ApplicationExceptionBase(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}