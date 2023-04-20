using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetFlights(int id)
        {
            return Ok();
        }
    }
}
