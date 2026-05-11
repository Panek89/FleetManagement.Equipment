using Microsoft.AspNetCore.Mvc;
using FleetManagement.Equipment.Infrastructure;

namespace FleetManagement.Equipment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly AppDbContext _context;

        public HealthCheckController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("db")]
        public async Task<IActionResult> CheckDbConnection()
        {
            var isAlive = await _context.Database.CanConnectAsync();
            if (isAlive)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }

        [HttpGet("api")]
        public IActionResult CheckApi()
        {
            return Ok();
        }
    }
}
