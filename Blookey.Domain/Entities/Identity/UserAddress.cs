using Blookey.Domain.Exceptions;
using Blookey.Domain.ValueObjects;

namespace Blookey.Domain.Entities.Identity;

public class UserAddress
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string AddressNumber { get; set; }
    public string? Complement { get; set; }
    public string Province { get; set; }
    public PostalCode PostalCode { get; set; }
    public string UserId { get; set; }

    public User? User { get; set; }  

    // EF Core
    //public UserAddress() { }

    public static UserAddress Create(string address, string addressNumber, string? complement, string province, string postalCode, string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new DomainException("UserId não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(address))
            throw new DomainException("Logradouro não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(addressNumber))
            throw new DomainException("Número não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(province))
            throw new DomainException("Bairro não pode ser vazio.");

        return new UserAddress
        {
            Address = address,
            AddressNumber = addressNumber,
            Complement = complement,
            Province = province,
            PostalCode = PostalCode.Create(postalCode), // ← VO valida o CEP
            UserId = userId
        };
    }
}