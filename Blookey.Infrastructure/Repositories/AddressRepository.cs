using Blookey.Domain.Entities.Identity;
using Blookey.Domain.Interfaces;
using Blookey.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blookey.Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly BlookeyContext _context;

    public AddressRepository(BlookeyContext context)
    {
        _context = context; 
    }

    public async Task AddAsync(UserAddress address, CancellationToken cancellationToken = default)
    {
        await _context.UserAddresses.AddAsync(address, cancellationToken);
    }

    public async Task<UserAddress> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserAddresses.FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
    }
}
