using Blookey.Domain.Entities.Identity;

namespace Blookey.Domain.Interfaces;

public interface IAddressRepository
{
    Task AddAsync(UserAddress address, CancellationToken cancellationToken = default);  
    Task<UserAddress> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default);
}
