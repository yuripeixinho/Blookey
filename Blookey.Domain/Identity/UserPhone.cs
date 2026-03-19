using Blookey.Domain.Enumerations;

namespace Blookey.Domain.Identity;

public class UserPhone
{
    public int Id { get; private set; } 
    public string Phone {  get; private set; }
    public int PhoneTypeId { get; private set; }

    public PhoneType? PhoneType { get; private set; }    

    public UserPhone() { } // Para o EF Core

    public UserPhone(string phone, int phoneTypeId)
    {
        Phone = phone;
        PhoneTypeId = phoneTypeId;
    }   
}
