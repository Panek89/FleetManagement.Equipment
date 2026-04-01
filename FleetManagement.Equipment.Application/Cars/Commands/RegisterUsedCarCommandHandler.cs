using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Domain.Repositories;
using FleetManagement.Equipment.Domain.ValueObjects;
using MediatR;

namespace FleetManagement.Equipment.Application.Cars.Commands;

public record RegisterUsedCarCommand(Guid ManufacturerId, Money InitialValue, Money CurrentValue, string Title, string Description, decimal Mileage, int ProductionYear) : IRequest;

public class RegisterUsedCarCommandHandler : IRequestHandler<RegisterUsedCarCommand>
{
  private readonly ICarsRepository _carsRepository;

  public RegisterUsedCarCommandHandler(ICarsRepository carsRepository)
  {
    _carsRepository = carsRepository ?? throw new ArgumentNullException(nameof(carsRepository));
  }

  public async Task Handle(RegisterUsedCarCommand command, CancellationToken cancellationToken)
  {
    var usedCar = Car.RegisterUsed(command.ManufacturerId, command.InitialValue, command.CurrentValue, command.Title, command.Description, command.Mileage, command.ProductionYear);
    await _carsRepository.AddAsync(usedCar, cancellationToken);
    await _carsRepository.SaveChangesAsync(cancellationToken);
  }
}
