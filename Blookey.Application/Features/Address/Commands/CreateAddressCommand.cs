using Blookey.Domain.Identity;
using MediatR;

namespace Blookey.Application.Features.Address.Commands;

public record CreateAddressCommand(
    string Address, 
    string AddressNumber, 
    string? Complement, 
    string Province, 
    string PostalCode
) : IRequest<UserAddress>;