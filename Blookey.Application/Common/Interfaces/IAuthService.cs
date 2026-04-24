using Blookey.Application.Features.Identity.Dtos;

namespace Blookey.Application.Common.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(string email, string password);
    Task<RegisterResponse> RegisterAsync(RegisterRequest request); 
}
