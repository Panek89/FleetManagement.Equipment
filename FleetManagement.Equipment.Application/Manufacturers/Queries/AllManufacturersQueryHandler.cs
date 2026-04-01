using FleetManagement.Equipment.Domain.DTOs;
using FleetManagement.Equipment.Domain.Mappings;
using FleetManagement.Equipment.Domain.Repositories;
using MediatR;

namespace FleetManagement.Equipment.Application.Manufacturers.Queries;

public record AllManufacturersQuery() : IRequest<IEnumerable<ManufacturerDto>>;

public class AllManufacturersQueryHandler : IRequestHandler<AllManufacturersQuery, IEnumerable<ManufacturerDto>>
{
  private readonly IManufacturersRepository _manufacturersRepository;

  public AllManufacturersQueryHandler(IManufacturersRepository manufacturersRepository)
  {
    _manufacturersRepository = manufacturersRepository ?? throw new ArgumentNullException(nameof(manufacturersRepository));
  }

  public async Task<IEnumerable<ManufacturerDto>> Handle(AllManufacturersQuery query, CancellationToken cancellationToken)
  {
    return (await _manufacturersRepository.GetAllAsync(cancellationToken)).MapToDtos();
  }
}
