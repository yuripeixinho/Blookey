using Blookey.Domain.Entities.Identity;

namespace Blookey.Application.Features.Address.Dtos;

public sealed record UserAddressDto(
    string Address,
    string AddressNumber,
    string? Complement,
    string Province,
    string PostalCode)
{
    public static UserAddressDto FromEntity(UserAddress userAddress)
    {
        return new UserAddressDto(
            userAddress.Address,
            userAddress.AddressNumber,
            userAddress.Complement,
            userAddress.Province,
            userAddress.PostalCode.Formatted
        );
    }
}