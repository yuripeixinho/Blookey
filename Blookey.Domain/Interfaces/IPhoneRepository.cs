using Blookey.Domain.Entities.Identity;

namespace Blookey.Domain.Interfaces;

public interface IPhoneRepository
{
    Task AddAsync(UserPhone phone, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string phoneNumber, string userId, CancellationToken cancellationToken = default);
}
