using Blookey.Domain.Entities.Identity;
using Blookey.Domain.Interfaces;
using Blookey.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blookey.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BlookeyContext _context;

    public UserRepository(BlookeyContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    }

    public async Task UpdateOnboardingInfoAsync(
        string userId, 
        string assasId, 
        string walletId, 
        string agency, 
        string account, 
        string accountDigit, 
        CancellationToken cancellationToken)
    {
        await _context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s
            .SetProperty(u => u.AssasId, assasId)
            .SetProperty(u => u.WalletId, walletId)
            .SetProperty(u => u.AccountAgency, agency)
            .SetProperty(u => u.AccountNumber, account)
            .SetProperty(u => u.AccountDigit, accountDigit),
            cancellationToken);
    }
}


