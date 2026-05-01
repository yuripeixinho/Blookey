using Blookey.Domain.Exceptions;

namespace Blookey.Domain.ValueObjects;

public sealed class PhoneNumber
{
    public string Value { get; }

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber Create(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new DomainException("Telefone não pode ser vazio.");

        var digits = new string(phone.Where(char.IsDigit).ToArray());

        if (digits.Length < 10 || digits.Length > 11)
            throw new DomainException("Telefone deve ter entre 10 e 11 dígitos.");

        return new PhoneNumber(digits);
    }

    public override string ToString() => Value;
}
