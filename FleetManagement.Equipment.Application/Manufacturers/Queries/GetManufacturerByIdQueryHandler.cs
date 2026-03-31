using FleetManagement.Equipment.Domain.Entities;
using FleetManagement.Equipment.Domain.Repositories;
using MediatR;

namespace FleetManagement.Equipment.Application.Manufacturers.Queries;

public record GetManufacturerByIdQuery(Guid Id) : IRequest<Manufacturer?>;

public class GetManufacturerByIdQueryHandler : IRequestHandler<GetManufacturerByIdQuery, Manufacturer?>
{
  private readonly IManufacturersRepository _manufacturersRepository;

  public GetManufacturerByIdQueryHandler(IManufacturersRepository manufacturersRepository)
  {
    _manufacturersRepository = manufacturersRepository ?? throw new ArgumentNullException(nameof(manufacturersRepository));
  }

  public async Task<Manufacturer?> Handle(GetManufacturerByIdQuery query, CancellationToken cancellationToken)
  {
    return await _manufacturersRepository.GetByIdAsync(query.Id, cancellationToken);
  }
}
