using FleetManagement.Equipment.Domain.DTOs;
using FleetManagement.Equipment.Domain.Entities;

namespace FleetManagement.Equipment.Domain.Mappings;

public static class CarMappings
{
  public static CarDto MapToDto(this Car car)
  {
    return new CarDto()
    {
      Id = car.Id,
      Mileage = car.Mileage,
      ProductionYear = car.ProductionYear,
      ManufacturerName = car.Manufacturer.Name,
      ManufacturerCountry = car.Manufacturer.Country,
      CurrentValue = car.CurrentValue.Amount
    };
  }

  public static IEnumerable<CarDto> MapToDtos(this IEnumerable<Car> cars)
  {
    return cars.Select(MapToDto);
  }
}
