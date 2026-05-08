using Blookey.Domain.Entities.Identity;
using Blookey.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blookey.Infrastructure.Data.Identity;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CpfCnpj)
           .HasConversion(
               cc => cc.Value, // Converte CpfCnpj para string ao salvar no banco    
               value => CpfCnpj.Create(value) // CpfCnpj string para PostalCode ao ler do banco
           )
           .IsRequired()
           .HasMaxLength(14);

        builder.Property(x => x.BirthDate)
               .HasColumnType("date");   // Opcional: Salva apenas a data (sem a hora) para economizar espaço e evitar bugs de fuso horário

        builder.Property(x => x.IncomeValue)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.AssasId)
            .HasMaxLength(50);

        builder.Property(x => x.WalletId)
            .HasMaxLength(50);

        builder.Property(x => x.AssasApiKeyCipher)
            .HasMaxLength(500);

        builder.Property(x => x.AccountAgency)
            .HasMaxLength(10);

        builder.Property(x => x.AccountNumber)
            .HasMaxLength(20);

        builder.Property(x => x.AccountDigit)
            .HasMaxLength(2);
    }
}