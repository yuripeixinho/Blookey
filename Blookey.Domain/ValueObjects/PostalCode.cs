using Blookey.Domain.Exceptions;

namespace Blookey.Domain.ValueObjects;

public sealed class PostalCode
{
    public string Value { get; }
    private PostalCode(string value) => Value = value; // Ninguém de fora consegue fazer new PostalCode("..."). A única porta de entrada é o Create. Isso garante que nenhum PostalCode inválido pode existir — é impossível criar um sem passar pelas validações.

    public static PostalCode Create(string postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new DomainException("CEP não pode ser vazio.");

        var digits = new string(postalCode.Where(char.IsDigit).ToArray()); //   filtra só os dígitos — aceita "01310-100" ou "01310100"

        if (digits.Length != 8)
            throw new DomainException("CEP deve conter exatamente 8 dígitos."); 

        return new PostalCode(digits); // persiste sem traço: "01310100"
    }

    public string Formatted => $"{Value[..5]}-{Value[5..]}"; // "01310-100"
}
