using FleetManagement.Equipment.Domain.DTOs;
using FleetManagement.Equipment.Domain.Entities;

namespace FleetManagement.Equipment.Domain.Mappings;

public static class ManufacturerMappings
{
  public static ManufacturerDto MapToDto(this Manufacturer manufacturer)
  {
    return new ManufacturerDto()
    {
      Id = manufacturer.Id,
      Name = manufacturer.Name,
      Country = manufacturer.Country,
      IsActive = manufacturer.IsActive
    };
  }
}
