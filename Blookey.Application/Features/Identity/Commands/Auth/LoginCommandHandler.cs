using Blookey.Application.Common.Interfaces;
using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Auth;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IAuthService _authService;

    public LoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var token = await _authService.LoginAsync(command.Email, command.Password);
       
        if(string.IsNullOrEmpty(token))
            throw new Exception("Credenciais inválidas"); // Ou use Notification Pattern / Result Pattern

        return new LoginResponse(token, command.Email);
    }
}
