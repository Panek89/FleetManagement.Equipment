using FleetManagement.Equipment.Shared.Consts;

namespace FleetManagement.Equipment.Domain.Entities;

public abstract class BaseEntity
{
  public Guid Id { get; set; } = Guid.NewGuid();
  public bool IsActive { get; set; } = false;
  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public string CreatedBy { get; set; } = DefaultValues.SYSTEM;
  public DateTime? UpdatedAt { get; set; } = null;
  public string? UpdatedBy { get; set; } = null;

  public BaseEntity() { }

  public BaseEntity(bool isActive, string createdBy)
  {
    IsActive = isActive;
    CreatedBy = createdBy;
  }
}
