using FlightPlanner.API.Models;
using FlightPlanner.API.Validation;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FlightPlanner.API.Controllers
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminApiController : BaseApiController
    {
        private static readonly object _flightLock = new object();
        private readonly IFlightService _flightService;
        public AdminApiController(
            IFlightPlannerDbContext context,
            IFlightService flightService) : base(context)
        {
            _flightService = flightService;
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFullFlight(id);

            return Ok(flight);
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest flight)
        {
            if (flight == null) return BadRequest();
            
            if (ValidateFlight.HasInvalidFlightDetails(flight) ||
                ValidateFlight.HasInvalidAirport(flight) ||
                ValidateFlight.HasInvalidFlightTime(flight))
                return BadRequest();

            Flight newFlight = null;

            lock (_flightLock)
            {
                var existingFlight = _context.Flights
                    .Any(f => f.From.AirportCode == flight.From.Airport &&
                              f.To.AirportCode == flight.To.Airport &&
                              f.Carrier == flight.Carrier &&
                              f.DepartureTime == flight.DepartureTime &&
                              f.ArrivalTime == flight.ArrivalTime);

                if (existingFlight) return Conflict();

                newFlight = new Flight
                {
                    From = flight.From,
                    To = flight.To,
                    Carrier = flight.Carrier,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime
                };

                _context.Flights.Add(newFlight);
                _context.SaveChanges();
            }

            return Created("", newFlight);
        }

        [HttpDelete]
        [Route("flights/{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            

            return Ok();
        }
    }
}