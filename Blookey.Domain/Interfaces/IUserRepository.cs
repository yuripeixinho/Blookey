using Blookey.Domain.Entities.Identity;

namespace Blookey.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(string userId, CancellationToken cancellationToken = default);

    Task UpdateOnboardingInfoAsync(string userId,
    string assasId,
    string walletId,
    string agency,
    string account,
    string accountDigit,
    CancellationToken cancellationToken);
}
