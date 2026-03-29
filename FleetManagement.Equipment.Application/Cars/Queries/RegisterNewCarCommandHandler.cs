using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Domain.Repositories;
using FleetManagement.Equipment.Domain.ValueObjects;
using MediatR;

namespace FleetManagement.Equipment.Application.Cars.Queries;

public record RegisterNewCarCommand(Guid ManufacturerId, Money InitialValue, string Title, string Description) : IRequest;

public class RegisterNewCarCommandHandler : IRequestHandler<RegisterNewCarCommand>
{
  private readonly ICarsRepository _carsRepository;

  public RegisterNewCarCommandHandler(ICarsRepository carsRepository)
  {
    _carsRepository = carsRepository ?? throw new ArgumentNullException(nameof(carsRepository));
  }

  public async Task Handle(RegisterNewCarCommand command, CancellationToken cancellationToken)
  {
    var newCar = Car.RegisterNew(command.ManufacturerId, command.InitialValue, command.Title, command.Description);
    await _carsRepository.AddAsync(newCar, cancellationToken);
    await _carsRepository.SaveChangesAsync(cancellationToken);
  }
}
