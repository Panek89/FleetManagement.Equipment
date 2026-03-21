using FleetManagement.Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Equipment.Infrastructure.Configurations;

public class ManufacturerConfiguration
  : BaseEntityConfiguration<Manufacturer>, IEntityTypeConfiguration<Manufacturer>
{
  public override void Configure(EntityTypeBuilder<Manufacturer> builder)
  {
    base.Configure(builder);

    builder.ToTable("MANUFACTURERS");

    builder.Property(p => p.Name).HasColumnName("NAME");
    builder.Property(p => p.Country).HasColumnName("COUNTRY");

    builder.HasMany(f => f.Cars)
      .WithOne(m => m.Manufacturer)
      .HasForeignKey(fk => fk.ManufacturerId)
      .OnDelete(DeleteBehavior.Cascade);
  }
}
