using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Auth;

public sealed record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
public sealed record LoginResponse(string Token, string Email); 