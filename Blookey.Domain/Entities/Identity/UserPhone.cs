using Blookey.Domain.Enumerations;
using Blookey.Domain.Exceptions;
using Blookey.Domain.ValueObjects;

namespace Blookey.Domain.Entities.Identity;

public class UserPhone
{
    public int Id { get; private set; }
    public PhoneNumber Phone { get; private set; }
    public int PhoneTypeId { get; private set; }
    public string UserId { get; private set; }

    public PhoneType? PhoneType { get; private set; }
    public User? User { get; set; }

    //public UserPhone() { } // Para o EF Core

    public static UserPhone Create(string phone, int phoneTypeId, string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new DomainException("UserId não pode ser vazio.");

        if (phoneTypeId <= 0)
            throw new DomainException("O tipo de telefone é inválido.");

        return new UserPhone
        {
            Phone = PhoneNumber.Create(phone), // ← validação do VO
            PhoneTypeId = phoneTypeId,
            UserId = userId
        };
    }
}
