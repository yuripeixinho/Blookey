using MediatR;

namespace Blookey.Application.Features.Auth.Commands;

public record RegisterCommand(string Username, string Email, string Password) 
    : IRequest<string>;