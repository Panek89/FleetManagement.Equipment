using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Equipment.Infrastructure.Repositories;

public class CarsRepository : BaseRepository<Car>, ICarsRepository
{
  public CarsRepository(AppDbContext context) : base(context)
  {
  }

  public async Task<IEnumerable<Car>> GetByManufacturerAsync(string manufacturerName, CancellationToken cancellationToken)
  {
    return await _dbSet.Include(x => x.Manufacturer).Where(y => y.Manufacturer.Name == manufacturerName).ToListAsync(cancellationToken);
  }
}
