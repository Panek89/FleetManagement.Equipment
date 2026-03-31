using FleetManagement.Equipment.Application.Manufacturers.Queries;
using FleetManagement.Equipment.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Equipment.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ManufacturersController : ControllerBase
  {
    private readonly ISender _sender;

    public ManufacturersController(ISender sender)
    {
      _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    [HttpGet("get-by-id/{id:guid}")]
    public async Task<ActionResult<Manufacturer?>> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
      return await _sender.Send(new GetManufacturerByIdQuery(id), cancellationToken);
    }
  }
}
