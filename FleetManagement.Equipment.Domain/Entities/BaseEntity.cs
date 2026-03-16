namespace FleetManagement.Equipment.Domain.Entities;

public class BaseEntity<T> where T : class
{
  public Guid Id { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }
  public required string CreatedBy { get; set; }
  public DateTime UpdatedAt { get; set; }
  public required string UpdatedBy { get; set; }
}
