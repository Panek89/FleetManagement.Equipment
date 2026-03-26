using FleetManagement.Equipment.Domain.ValueObjects;

namespace FleetManagement.Equipment.Domain.Entities;

public sealed class Car : BaseEquipment<Car>
{
  public Guid ManufacturerId { get; set; }
  public Manufacturer Manufacturer { get; set; } = null!;
  public decimal Mileage { get; set; }
  public int ProductionYear { get; set; }

  private Car() { }

  private Car(
    Money initialValue,
    Money currentValue,
    string title,
    string description,
    Guid manufacturerId,
    decimal mileage,
    int productionYear
  )
    : base(initialValue, currentValue, title, description)
  {
    ManufacturerId = manufacturerId;
    Mileage = mileage;
    ProductionYear = productionYear;
  }

  public Car Make(Guid manufacturerId, Money initialValue, string title, string description)
  {
    var currentYearProduction = DateTime.Now.Year;

    return new Car(initialValue, initialValue, title, description, manufacturerId, 0, currentYearProduction);
  }
}
