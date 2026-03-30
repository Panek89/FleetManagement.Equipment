using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Shared.Consts;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Equipment.Infrastructure.SeedData;

public static class ManufacturersSeeder
{
  public static async Task SeedAsync(AppDbContext context, CancellationToken cancellationToken)
  {
    foreach (var (id, name, country) in DefaultValues.DEFAULT_MANUFACTURERS)
    {
      var isExists = await context.Manufacturers.AnyAsync(x => x.Id.ToString() == id);
      if (!isExists)
      {
        var newManufacturer = new Manufacturer(name, country, true, DefaultValues.SEEDER)
        {
          Id = Guid.Parse(id)
        };
        await context.Manufacturers.AddAsync(newManufacturer);
      }
    }

    await context.SaveChangesAsync();
  }
}
