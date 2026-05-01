using Blookey.Domain.Entities.Identity;
using Blookey.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blookey.Infrastructure.Data.Identity;

public class UserPhoneConfiguration : IEntityTypeConfiguration<UserPhone>
{
    public void Configure(EntityTypeBuilder<UserPhone> builder)
    {
        builder.ToTable("users_phones");

        builder.HasKey(x => x.Id);

        // ← Ensina o EF Core a converter PhoneNumber para string e vice-versa
        builder.Property(x => x.Phone)
            .HasConversion(
                phone => phone.Value,           // salva: PhoneNumber → string
                value => PhoneNumber.Create(value) // lê: string → PhoneNumber
            )
            .IsRequired()
            .HasMaxLength(20);

        // Relacionamentos
        builder.HasOne(x => x.PhoneType)
            .WithMany()
            .HasForeignKey(x => x.PhoneTypeId)
            .OnDelete(DeleteBehavior.Restrict); // Evita deletar um tipo de telefone em uso

        builder.HasOne(x => x.User)
            .WithMany(u => u.Phones) // Conecta com a ICollection no User
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade); 

        // Índices (Opcional, mas recomendado para buscas rápidas)
        builder.HasIndex(x => x.Phone);
    }
}