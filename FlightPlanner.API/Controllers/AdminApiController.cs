using AutoMapper;
using FlightPlanner.API.Models;
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
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;

        public AdminApiController(
            IFlightPlannerDbContext context,
            IFlightService flightService,
            IMapper mapper) : base(context)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFullFlight(id);

            if (flight == null) return NotFound();

            return Ok(_mapper.Map<AddFlightRequest>(flight));
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest request)
        {
            var flight = _mapper.Map<Flight>(request);

            if (flight == null) return BadRequest();

            if (_flightService.HasInvalidAirport(flight) ||
                _flightService.HasInvalidFlightDetails(flight) ||
                _flightService.HasInvalidFlightTime(flight))
                return BadRequest();

            if (_flightService.FlightExists(flight)) return Conflict();

            _flightService.Create(flight);

            return Created("", _mapper.Map<AddFlightRequest>(flight));
        }

        [HttpDelete]
        [Route("flights/{id:int}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _flightService.Get(id);

            if (flight == null) return NotFound();

            _flightService.Delete(flight);
            return Ok();
        }
    }
}