using FleetManagement.Equipment.Domain.DTOs;
using FleetManagement.Equipment.Domain.Mappings;
using FleetManagement.Equipment.Domain.Repositories;
using MediatR;

namespace FleetManagement.Equipment.Application.Manufacturers.Queries;

public record GetManufacturerByIdQuery(Guid Id) : IRequest<ManufacturerDto?>;

public class GetManufacturerByIdQueryHandler : IRequestHandler<GetManufacturerByIdQuery, ManufacturerDto?>
{
  private readonly IManufacturersRepository _manufacturersRepository;

  public GetManufacturerByIdQueryHandler(IManufacturersRepository manufacturersRepository)
  {
    _manufacturersRepository = manufacturersRepository ?? throw new ArgumentNullException(nameof(manufacturersRepository));
  }

  public async Task<ManufacturerDto?> Handle(GetManufacturerByIdQuery query, CancellationToken cancellationToken)
  {
    var manufacturer = await _manufacturersRepository.GetByIdAsync(query.Id, cancellationToken);
    return manufacturer is not null ? manufacturer.MapToDto() : null;
  }
}
