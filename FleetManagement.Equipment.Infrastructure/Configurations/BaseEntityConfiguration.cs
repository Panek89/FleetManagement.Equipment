using FleetManagement.Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FleetManagement.Equipment.Infrastructure.Configurations;

public class BaseEntityConfiguration<T> where T : BaseEntity
{
  public virtual void Configure(EntityTypeBuilder<T> builder)
  {
    builder.HasKey(x => x.Id);
  }
}
