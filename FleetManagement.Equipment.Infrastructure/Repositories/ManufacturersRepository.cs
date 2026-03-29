using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Domain.Repositories;

namespace FleetManagement.Equipment.Infrastructure.Repositories;

public class ManufacturersRepository : BaseRepository<Manufacturer>, IManufacturersRepository
{
  public ManufacturersRepository(AppDbContext context) : base(context)
  {
  }
}
