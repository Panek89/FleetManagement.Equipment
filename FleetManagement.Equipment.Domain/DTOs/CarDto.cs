namespace FleetManagement.Equipment.Domain.DTOs;

public class CarDto
{
  public Guid Id { get; init; }
  public decimal Mileage { get; init; }
  public int ProductionYear { get; init; }
  public required string ManufacturerName { get; init; }
  public required string ManufacturerCountry { get; init; }
  public decimal CurrentValue { get; init; }
}
