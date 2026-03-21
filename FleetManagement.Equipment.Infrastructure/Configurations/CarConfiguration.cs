using FleetManagement.Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Equipment.Infrastructure.Configurations;

public class CarConfiguration : BaseEquipmentConfiguration<Car>, IEntityTypeConfiguration<Car>
{
  public override void Configure(EntityTypeBuilder<Car> builder)
  {
    base.Configure(builder);

    builder.ToTable("CARS");

    builder.Property(p => p.ManufacturerId).HasColumnName("MANUFACTURER_ID");
    builder.Property(p => p.ProductionYear).HasColumnName("PRODUCTION_YEAR");
    builder.Property(p => p.Mileage)
      .HasColumnName("MILEAGE")
      .HasColumnType("decimal")
      .HasPrecision(2);
  }
}
