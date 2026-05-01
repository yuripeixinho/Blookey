namespace Blookey.Domain.Entities.Identity;

public class UserAddress
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string AddressNumber { get; set; }
    public string? Complement { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public string UserId { get; set; }

    public User? User { get; set; }  

    // EF Core
    public UserAddress() { }

    public UserAddress(string address, string addressNumber, string? complement, string province, string postalCode, string userId)
    {
        Address = address;
        AddressNumber = addressNumber;  
        Complement = complement;
        Province = province;
        PostalCode = postalCode;
        UserId = userId;
    }
}