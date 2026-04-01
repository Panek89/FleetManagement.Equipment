using FleetManagement.Equipment.Application.Cars.Commands;
using FleetManagement.Equipment.Application.Cars.Queries;
using FleetManagement.Equipment.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FleetManagement.Equipment.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CarsController : ControllerBase
  {
    private readonly ISender _sender;

    public CarsController(ISender sender)
    {
      _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    }

    [HttpGet("by-manufacturer/{manufacturerName}")]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetByManufacturer(string manufacturerName, CancellationToken cancellationToken)
    {
      return Ok(await _sender.Send(new CarsByManufacturerQuery(manufacturerName), cancellationToken));
    }

    [HttpPost("register-new")]
    public async Task<IActionResult> RegisterNew([FromBody] RegisterNewCarCommand command)
    {
      await _sender.Send(command);

      return Ok();
    }

    [HttpPost("register-used")]
    public async Task<IActionResult> RegisterUsed([FromBody] RegisterUsedCarCommand command)
    {
      await _sender.Send(command);

      return Ok();
    }
  }
}
