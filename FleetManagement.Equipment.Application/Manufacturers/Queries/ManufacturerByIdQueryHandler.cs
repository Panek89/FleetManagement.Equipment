using FleetManagement.Equipment.Domain.DTOs;
using FleetManagement.Equipment.Domain.Mappings;
using FleetManagement.Equipment.Domain.Repositories;
using MediatR;

namespace FleetManagement.Equipment.Application.Manufacturers.Queries;

public record ManufacturerByIdQuery(Guid Id) : IRequest<ManufacturerDto?>;

public class ManufacturerByIdQueryHandler : IRequestHandler<ManufacturerByIdQuery, ManufacturerDto?>
{
  private readonly IManufacturersRepository _manufacturersRepository;

  public ManufacturerByIdQueryHandler(IManufacturersRepository manufacturersRepository)
  {
    _manufacturersRepository = manufacturersRepository ?? throw new ArgumentNullException(nameof(manufacturersRepository));
  }

  public async Task<ManufacturerDto?> Handle(ManufacturerByIdQuery query, CancellationToken cancellationToken)
  {
    var manufacturer = await _manufacturersRepository.GetByIdAsync(query.Id, cancellationToken);
    return manufacturer is not null ? manufacturer.MapToDto() : null;
  }
}
