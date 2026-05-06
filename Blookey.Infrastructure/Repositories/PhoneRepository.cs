using Blookey.Domain.Entities.Identity;
using Blookey.Domain.Interfaces;
using Blookey.Domain.ValueObjects;
using Blookey.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blookey.Infrastructure.Repositories;

public class PhoneRepository : IPhoneRepository
{
    private readonly BlookeyContext _context;

    public PhoneRepository(BlookeyContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserPhone phone, CancellationToken cancellationToken = default)
    {
        await _context.UserPhones.AddAsync(phone, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string phone, string userId, CancellationToken cancellationToken = default)
    {
        var phoneVo = PhoneNumber.Create(phone);

        return await _context.UserPhones.AnyAsync(p => p.Phone == phoneVo && p.UserId == userId, cancellationToken);
    }

    public async Task<UserPhone> GetByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await _context.UserPhones.FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);  
    }
}
