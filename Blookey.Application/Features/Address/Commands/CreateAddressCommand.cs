using Blookey.Application.Features.Address.Dtos;
using Blookey.Domain.Common;
using MediatR;

namespace Blookey.Application.Features.Address.Commands;

public sealed record CreateAddressCommand(
    string Address, 
    string AddressNumber, 
    string? Complement, 
    string Province, 
    string PostalCode
) : IRequest<Result<UserAddressDto>>;