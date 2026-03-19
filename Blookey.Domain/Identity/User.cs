using Blookey.Domain.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace Blookey.Domain.Identity;

public class User : IdentityUser
{
    public string Name { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public decimal? IncomeValue { get; private set; }
    public int? CompanyTypeId { get; private set; }

    public CompanyType? CompanyType { get; private set; }
    public ICollection<UserPhone> Phones { get; private set; } = [];
    public ICollection<UserAddress> Addresses { get; private set; } = [];

    // EF Core
    public User() { }

    public User(string name, string email)
    {
        Name = name;
        Email = email;
        UserName = email;
    }
}
