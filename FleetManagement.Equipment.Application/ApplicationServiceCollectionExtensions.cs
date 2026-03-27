using Microsoft.Extensions.DependencyInjection;

namespace FleetManagement.Equipment.Application;

public static class ApplicationServiceCollectionExtensions
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IApplicationMarker>());

    return services;
  }
}
