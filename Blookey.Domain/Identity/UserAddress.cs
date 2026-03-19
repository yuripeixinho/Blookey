namespace Blookey.Domain.Identity;

public class UserAddress
{
    public int Id { get; set; } 
    public string Address { get; set; }
    public string AddressNumber { get; set; }
    public string? Complement { get; set; }
    public string Province { get; set; }
    public string PostalCode { get; set; }
    public required Guid UserId { get; set; }

    public User? User { get; set; }  

    // EF Core
    public UserAddress() { }

    public UserAddress(int id, string address, string addressNumber, string? complement, string province, string postalCode, Guid userId)
    {
        Id = id;
        Address = address;
        AddressNumber = addressNumber;  
        Complement = complement;
        Province = province;
        PostalCode = postalCode;
        UserId = userId;
    }
}
