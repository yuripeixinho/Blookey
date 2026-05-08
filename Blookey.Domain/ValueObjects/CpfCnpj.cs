using Blookey.Domain.Exceptions;

namespace Blookey.Domain.ValueObjects;

public sealed class CpfCnpj 
{
    public string Value { get; }

    // Construtor privado para garantir o uso do Factory Method ou a criação controlada
    private CpfCnpj(string value)
    {
        Value = value;
    }

    public static CpfCnpj Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("CPF ou CNPJ não pode ser vazio.");

        var digits = new string(value.Where(char.IsDigit).ToArray());

        return new CpfCnpj(digits); 
    }

    public bool IsCpf => Value.Length == 11;
    public bool IsCnpj => Value.Length == 14;
}

