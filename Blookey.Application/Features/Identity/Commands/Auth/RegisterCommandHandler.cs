using Blookey.Application.Common.Interfaces;
using Blookey.Application.Features.Identity.Dtos;
using MediatR;

namespace Blookey.Application.Features.Identity.Commands.Auth;


public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string>
{
    private readonly IAuthService _authService;

    public RegisterCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerReq = new RegisterRequest(
            request.Name,
            request.CpfCnpj,
            request.BirthDate,
            request.IncomeValue,
            request.Email,
            request.Password,
            request.ConfirmPassword
        );

        var result = await _authService.RegisterAsync(registerReq);

        return result.UserId;
    }
}
