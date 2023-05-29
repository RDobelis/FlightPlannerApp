﻿using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.API.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class CleanupApiController : BaseApiController
    {
        public CleanupApiController(FlightPlannerDbContext context) : base(context)
        {
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();

            return Ok();
        }
    }
}