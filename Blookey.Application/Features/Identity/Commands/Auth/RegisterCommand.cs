using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Auth;

public sealed record RegisterCommand(
    string Name,
    string CPF,
    DateTime BirthDate,
    string Email,
    string Password,
    string ConfirmPassword) : IRequest<string>;