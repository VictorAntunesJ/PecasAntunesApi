using System;
using Microsoft.AspNetCore.Mvc;

namespace PecasAntunes.API.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "UP",
                application = "PecasAntunes API",
                timestamp = DateTime.UtcNow
            });
        }
    }
}
