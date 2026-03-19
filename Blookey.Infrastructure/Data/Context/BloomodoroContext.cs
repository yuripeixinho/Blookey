using Blookey.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blookey.Infrastructure.Data.Context;

public class BlookeyContext : IdentityDbContext<User>
{
    public BlookeyContext(DbContextOptions<BlookeyContext> options) : base(options)   
    {}

    //public DbSet<PlantSpecies> PlantSpecies { get; private set; }
    //public DbSet<GrowthStage> GrowthStages { get; private set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 1. ESTA LINHA É OBRIGATÓRIA!
        // Ela configura as chaves primárias do Identity (UserLogin, UserRole, etc.)
        base.OnModelCreating(modelBuilder);

        // 2. Depois dela, você aplica suas configurações personalizadas
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlookeyContext).Assembly);
        //DimBancoSeed.Seed(modelBuilder);
        //DimCodigoInscricaoSeed.Seed(modelBuilder);
    }
}
