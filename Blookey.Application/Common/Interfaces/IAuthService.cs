namespace Blookey.Application.Common.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(string email, string password);
    Task<string> RegisterAsync(string username, string email, string password);
}
