namespace FleetManagement.Equipment.Domain.DTOs;

public class ManufacturerDto
{
  public Guid Id { get; init; }
  public required string Name { get; init; }
  public required string Country { get; init; }
  public bool IsActive { get; init; }
}
