using Blookey.Domain.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace Blookey.Infrastructure.Data.Context;

public static class Seeds
{
    public static void PhoneTypeSeed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhoneType>().HasData(
            new { Id = 1, Name = "Celular"},
            new { Id = 2, Name = "Fixo" },
            new { Id = 3, Name = "WhatsApp"}
        );
    }

}
