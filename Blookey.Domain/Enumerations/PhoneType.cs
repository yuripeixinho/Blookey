namespace Blookey.Domain.Enumerations;

public class PhoneType
{
    public  int Id { get; private set; }    
    public string Name { get; private set; }

    public PhoneType() { } // Para o EF Core    

    public PhoneType(int id, string name)
    {
        Id = id;
        Name = name;    
    }
}
