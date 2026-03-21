using FleetManagement.Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Equipment.Infrastructure.Configurations;

public class BaseEquipmentConfiguration<T> : BaseEntityConfiguration<T>
  where T : BaseEquipment<T>
{
  public override void Configure(EntityTypeBuilder<T> builder)
  {
    base.Configure(builder);

    builder.Property(p => p.Title).HasColumnName("TITLE");
    builder.Property(p => p.Description).HasColumnName("DESCRIPTION");

    builder.OwnsOne(o => o.InitialValue, initialValueBuilder =>
    {
      initialValueBuilder.Property(p => p.Amount)
        .HasColumnName("INITIAL_VALUE_AMOUNT")
        .HasColumnType("decimal")
        .HasPrecision(2);
      initialValueBuilder.Property(p => p.Currency).HasColumnName("INITIAL_VALUE_CURRENCY");
    });

    builder.OwnsOne(o => o.CurrentValue, currentValueBuilder =>
    {
      currentValueBuilder.Property(p => p.Amount)
        .HasColumnName("CURRENT_VALUE_AMOUNT")
        .HasColumnType("decimal")
        .HasPrecision(2);
      currentValueBuilder.Property(p => p.Currency).HasColumnName("CURRENT_VALUE_CURRENCY");
    });
  }
}
