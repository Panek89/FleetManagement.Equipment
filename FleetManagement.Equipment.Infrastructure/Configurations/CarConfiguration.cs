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

    builder.Property(p => p.Mileage).HasColumnType("decimal").HasPrecision(2);
  }
}
