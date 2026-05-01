using Blookey.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blookey.Infrastructure.Data.Identity;

public class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable("users_addresses");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Address).IsRequired().HasMaxLength(200);
        builder.Property(x => x.AddressNumber).IsRequired().HasMaxLength(20);
        builder.Property(x => x.Complement).HasMaxLength(100);
        builder.Property(x => x.Province).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PostalCode).IsRequired().HasMaxLength(15);

        builder.HasOne(x => x.User)
                .WithMany(u => u.Addresses) // Conecta com a ICollection no User
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
    }
}
