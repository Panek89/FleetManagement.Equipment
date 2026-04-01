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
    int productionYear,
    bool isActive
  )
    : base(initialValue, currentValue, title, description)
  {
    ManufacturerId = manufacturerId;
    Mileage = mileage;
    ProductionYear = productionYear;
    IsActive = isActive;
  }

  public static Car RegisterNew(Guid manufacturerId, Money initialValue, string title, string description)
  {
    var currentYearProduction = DateTime.Now.Year;
    var zeroMileage = 0;
    var currentValue = initialValue with { };

    return new Car(initialValue, currentValue, title, description, manufacturerId, zeroMileage, currentYearProduction, isActive: true);
  }

  public static Car RegisterUsed(Guid manufacturerId, Money initialValue, Money currentValue, string title, string description, decimal mileage, int productionYear)
  {
    return new Car(initialValue, currentValue, title, description, manufacturerId, mileage, productionYear, isActive: true);
  }
}
