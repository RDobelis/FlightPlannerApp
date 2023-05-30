using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : BaseApiController
    {
        private readonly ICustomerService _customerService;
        private readonly IAirportService _airportService;

        public CustomerApiController(
            IFlightPlannerDbContext context, 
            IAirportService airportService, 
            ICustomerService customerService) : base(context)
        {
            _airportService = airportService;
            _customerService = customerService;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirport(string search)
        {
            if (string.IsNullOrEmpty(search)) return BadRequest();

            var lowerSearch = search.Trim().ToLower();
            var result = _airportService.GetAllAirports(lowerSearch);

            return Ok(result);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(FlightSearch searchFlight)
        {
            if (_customerService.HasInvalidDetails(searchFlight) ||
                _customerService.MatchingAirport(searchFlight))
                return BadRequest();

            var response = _customerService.FindFlights(searchFlight);
            
            return Ok(response);
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _customerService.GetFullFlight(id);
            if (flight == null) return BadRequest();

            return Ok(flight);
        }
    }
}