namespace Blookey.Domain.Interfaces;

public interface IUserRepository
{
    Task<string> GetUserProfile(string userId);
}
