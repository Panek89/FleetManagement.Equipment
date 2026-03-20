using FleetManagement.Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Equipment.Infrastructure.Configurations;

public class BaseEquipmentConfiguration<T> : BaseEntityConfiguration<T>
  where T : BaseEquipment<T>
{
  public override void Configure(EntityTypeBuilder<T> builder)
  {
    builder.OwnsOne(o => o.InitialValue, initialValueBuilder =>
    {
      initialValueBuilder.Property(p => p.Amount).HasColumnType("decimal").HasPrecision(2);
      initialValueBuilder.Property(p => p.Currency);
    });

    builder.OwnsOne(o => o.CurrentValue, currentValueBuilder =>
    {
      currentValueBuilder.Property(p => p.Amount).HasColumnName("AMOUNT").HasColumnType("decimal").HasPrecision(2);
      currentValueBuilder.Property(p => p.Currency).HasColumnName("CURRENCY");
    });
  }
}
