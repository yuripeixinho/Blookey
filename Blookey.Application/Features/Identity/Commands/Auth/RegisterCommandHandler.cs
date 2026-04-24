using Blookey.Application.Common.Interfaces;
using Blookey.Application.Features.Identity.Dtos;
using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Auth;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthService _authService;
    private readonly IPublisher _publisher;

    public RegisterCommandHandler(IAuthService authService, IPublisher publisher)
    {
        _authService = authService;
        _publisher = publisher;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerReq = new RegisterRequest(
            request.Name,
            request.CPF,
            request.BirthDate,
            request.Email,
            request.Password,
            request.ConfirmPassword
        );

        var result = await _authService.RegisterAsync(registerReq);

        return result.UserId;
    }
}
