namespace Blookey.Application.Features.Address.Dtos;

public record CreateAddressRequest(
    string Address,
    string AddressNumber,
    string? Complement,
    string Province,
    string PostalCode
);
