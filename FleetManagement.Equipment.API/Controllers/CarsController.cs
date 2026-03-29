using FleetManagement.Equipment.Application.Cars.Queries;
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

    [HttpPost("register-new")]
    public async Task<IActionResult> RegisterNew([FromBody] RegisterNewCarCommand command)
    {
      await _sender.Send(command);

      return Ok();
    }

    [HttpPost("register-used")]
    public async Task<IActionResult> RegisterUsed([FromBody] RegisterNewCarCommand command)
    {
      await _sender.Send(command);

      return Ok();
    }
  }
}
