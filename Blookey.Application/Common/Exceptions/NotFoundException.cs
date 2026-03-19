namespace Blookey.Application.Common.Exceptions;

public class NotFoundException : ApplicationExceptionBase
{
    public NotFoundException(string message) : base(message)
    {

    }

    // Opção 2: Construtor inteligente (Padroniza a mensagem)
    // Exemplo de uso: throw new NotFoundException("Cliente", 123);
    // Mensagem gerada: "Entity \"Cliente\" (123) was not found."
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {

    }
}
