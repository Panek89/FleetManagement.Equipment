using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Infrastructure.Configurations;
using FleetManagement.Equipment.Infrastructure.SeedData;
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

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);
    optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
    {
      await ManufacturersSeeder.SeedAsync(this, cancellationToken);
    });
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfiguration(new CarConfiguration());
    modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
  }
}
