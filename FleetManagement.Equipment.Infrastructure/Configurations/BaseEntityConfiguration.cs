using FleetManagement.Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Equipment.Infrastructure.Configurations;

public class BaseEntityConfiguration<T> where T : BaseEntity
{
  public virtual void Configure(EntityTypeBuilder<T> builder)
  {
    builder.HasKey(x => x.Id);

    builder.Property(p => p.Id).HasColumnName("ID");
    builder.Property(p => p.IsActive).HasColumnName("IS_ACTIVE");
    builder.Property(p => p.CreatedAt).HasColumnName("CREATED_AT");
    builder.Property(p => p.CreatedBy).HasColumnName("CREATED_BY");
    builder.Property(p => p.UpdatedAt).HasColumnName("UPDATED_AT");
    builder.Property(p => p.UpdatedBy).HasColumnName("UPDATED_BY");
  }
}
