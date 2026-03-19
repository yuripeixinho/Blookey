namespace Blookey.Domain.Enumerations;

public class CompanyType
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    // Para o EF Core
    private CompanyType() { }

    private CompanyType(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
