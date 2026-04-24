using Blookey.Domain.Identity;
using Blookey.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blookey.Infrastructure.Data.Context;

public class BlookeyContext : IdentityDbContext<User>, IUnitOfWork
{
    public BlookeyContext(DbContextOptions<BlookeyContext> options) : base(options)   
    {}

    public DbSet<UserAddress> UserAddresses{ get; private set; }
    public DbSet<UserPhone> UserPhones { get; private set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ela configura as chaves primárias do Identity (UserLogin, UserRole, etc.)
        base.OnModelCreating(modelBuilder);

        // 2. Depois dela, você aplica suas configurações personalizadas
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlookeyContext).Assembly);

        //DimBancoSeed.Seed(modelBuilder);
        //DimCodigoInscricaoSeed.Seed(modelBuilder);
    }
}
