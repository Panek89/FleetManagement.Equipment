using FleetManagement.Equipment.Domain.DTOs;
using FleetManagement.Equipment.Domain.Mappings;
using FleetManagement.Equipment.Domain.Repositories;
using MediatR;

namespace FleetManagement.Equipment.Application.Cars.Queries;

public record CarsByManufacturerQuery(string Name) : IRequest<IEnumerable<CarDto>>;

public class CarsByManufacturerQueryHandler : IRequestHandler<CarsByManufacturerQuery, IEnumerable<CarDto>>
{
  private readonly ICarsRepository _carsRepository;

  public CarsByManufacturerQueryHandler(ICarsRepository carsRepository)
  {
    _carsRepository = carsRepository ?? throw new ArgumentNullException(nameof(carsRepository));
  }

  public async Task<IEnumerable<CarDto>> Handle(CarsByManufacturerQuery query, CancellationToken cancellationToken)
  {
    return (await _carsRepository.GetByManufacturerAsync(query.Name, cancellationToken)).MapToDtos();
  }
}
