using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Domain.Repositories;

namespace FleetManagement.Equipment.Infrastructure.Repositories;

public class CarsRepository : BaseRepository<Car>, ICarsRepository
{
  public CarsRepository(AppDbContext context) : base(context)
  {
  }
}
