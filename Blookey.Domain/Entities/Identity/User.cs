using Microsoft.AspNetCore.Identity;

namespace Blookey.Domain.Entities.Identity;

public class User : IdentityUser
{
    public string Name { get; set; }  
    public string CPF { get; set; }    
    public decimal IncomeValue { get; private set; }
    public DateTime BirthDate { get; private set; } 

    //public int? CompanyTypeId { get; private set; }

    //public CompanyType? CompanyType { get; private set; }  company type será um campo importante em um momento posterior quando tivermos vários clientes. Atualmente não vamos precisar dessa implementação.
    public ICollection<UserPhone> Phones { get; private set; } = [];
    public ICollection<UserAddress> Addresses { get; private set; } = [];

    // EF Core
    public User() { }

    //public User(string email)
    //{
    //    Email = email;
    //}
}
