using Blookey.Application.Features.Phone.Dtos;
using Blookey.Domain.Common;
using MediatR;

namespace Blookey.Application.Features.Phone.Commands;

public sealed record CreatePhoneCommand(
    string Phone,
    int PhoneType) : IRequest<Result<UserPhoneDto>>;
