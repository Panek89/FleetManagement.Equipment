using System;
using FleetManagement.Equipment.Domain.ValueObjects;

namespace FleetManagement.Equipment.Domain.Entities;

public sealed class Car : BaseEquipment<Car>
{
  public Manufacturer Manufacturer { get; set; } = null!;
  public decimal Mileage { get; set; }
  public int ProductionYear { get; set; }

  private Car() { }

  public Car(
    Money initialValue,
    Money currentValue,
    string title,
    string description,
    Manufacturer manufacturer,
    decimal mileage,
    int productionYear
  )
    : base(initialValue, currentValue, title, description)
  {
    Manufacturer = manufacturer;
    Mileage = mileage;
    ProductionYear = productionYear;
  }
}
