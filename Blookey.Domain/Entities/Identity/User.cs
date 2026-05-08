using Blookey.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Blookey.Domain.Entities.Identity;

public class User : IdentityUser
{
    public string Name { get; private set; }  
    public CpfCnpj CpfCnpj { get; private set; }
    public DateTime BirthDate { get; private set; }
    public decimal IncomeValue { get; private set; }

    #region Assas 
    public string? AccountAgency { get; private set; }
    public string? AccountNumber { get; private set; }
    public string? AccountDigit { get; private set; }
    public string? AssasApiKeyCipher { get; private set; }
    public string? AssasId { get; private set; }     
    public string? WalletId { get; private set; }
    #endregion

    public ICollection<UserPhone> Phones { get; private set; } = [];
    public ICollection<UserAddress> Addresses { get; private set; } = [];

    // EF Core
    public User() { }

    public static User Create(
        string name, 
        string cpfCnpj, 
        DateTime birthDate,
        decimal incomeValue,
        string email)
    {
        if (incomeValue < 0) throw new ArgumentException("Renda não pode ser negativa");

        return new User
        {
            Name = name,
            CpfCnpj = CpfCnpj.Create(cpfCnpj),
            BirthDate = birthDate,
            IncomeValue = incomeValue,
            Email = email,
            UserName = email,
            SecurityStamp = Guid.NewGuid().ToString()
        };
    }
}
