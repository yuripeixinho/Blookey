using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Auth;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
public record LoginResponse(string Token, string Email); 