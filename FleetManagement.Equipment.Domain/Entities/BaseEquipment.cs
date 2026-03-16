using FleetManagement.Equipment.Domain.ValueObjects;

namespace FleetManagement.Equipment.Domain.Entities;

public class BaseEquipment<T> : BaseEntity<T> where T : BaseEquipment<T>
{
  public Money InitialValue { get; set; }
  public required string Title { get; set; }
  public required string Description { get; set; }
}
