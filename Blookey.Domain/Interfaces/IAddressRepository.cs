using Blookey.Domain.Identity;

namespace Blookey.Domain.Interfaces;

public interface IAddressRepository
{
    Task AddAsync(UserAddress address, CancellationToken cancellationToken = default);  
}
