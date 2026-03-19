using Blookey.Application.Common.Interfaces;
using MediatR;

namespace Blookey.Application.Features.Auth.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService; 
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _authService.RegisterAsync(request.Username, request.Email, request.Password);
    }
}
