using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Infrastructure.Configurations;
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

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new CarConfiguration());
    modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
  }
}
