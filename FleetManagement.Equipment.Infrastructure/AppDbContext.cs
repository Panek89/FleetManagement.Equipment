using FleetManagement.Equipment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FleetManagement.Equipment.Infrastructure;

public class AppDbContext : DbContext
{
  public DbSet<Car> Cars { get; set; }
  public DbSet<Manufacturer> Manufacturers { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
  {
  }
}
