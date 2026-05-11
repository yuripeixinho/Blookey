using Blookey.Domain.Entities.Identity;
using Blookey.Domain.Interfaces;
using Blookey.Infrastructure.Data.Context;
using Blookey.Infrastructure.Exceptions.Semantics;
using Microsoft.EntityFrameworkCore;

namespace Blookey.Infrastructure.Repositories;

public class UserRepository(BlookeyContext context) : IUserRepository
{
    public async Task<User> GetByIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken) 
            ?? throw new EntityNotFoundException(nameof(User), userId);
    }

    public async Task UpdateOnboardingInfoAsync(
        string userId, 
        string assasId,
        string accessTokenId,
        string walletId, 
        string agency, 
        string account, 
        string accountDigit, 
        CancellationToken cancellationToken)
    {
        await context.Users
            .Where(u => u.Id == userId)
            .ExecuteUpdateAsync(s => s
            .SetProperty(u => u.AssasId, assasId)
            .SetProperty(u => u.WalletId, walletId)
            .SetProperty(u => u.AssasApiKeyCipher, accessTokenId)
            .SetProperty(u => u.AccountAgency, agency)
            .SetProperty(u => u.AccountNumber, account)
            .SetProperty(u => u.AccountDigit, accountDigit),
            cancellationToken);
    }
}


