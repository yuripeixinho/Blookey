using MediatR;

namespace Blookey.Application.Features.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<LoginResponse>;
public record LoginResponse(string Token, string Email); 