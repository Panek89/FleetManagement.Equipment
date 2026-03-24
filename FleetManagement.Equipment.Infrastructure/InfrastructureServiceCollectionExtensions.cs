using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.Equipment.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services)
  {
    // repositories

    return services;
  }

  public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
  {
    services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));

    return services;
  }
}
