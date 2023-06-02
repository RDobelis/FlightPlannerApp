using System.Collections.Generic;
using AutoMapper;
using FlightPlanner.API.Models;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Validations;
using FlightPlanner.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerApiController : BaseApiController
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IFlightSearchValidate _flightSearchValidator;

        public CustomerApiController(
            IFlightPlannerDbContext context,
            ICustomerService customerService,
            IFlightSearchValidate flightSearchValidator) : base(context)
        {
            _customerService = customerService;
            _flightSearchValidator = flightSearchValidator;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult GetAirport(string search)
        {
            var result = _customerService.GetAllAirports(search);

            if (result == null) return NotFound();

            List<AirportSearchResult> searchResult = new List<AirportSearchResult>();

            foreach (var airport in result)
            {
                searchResult.Add(_mapper.Map<AirportSearchResult>(airport));
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlight(FlightSearch searchFlight)
        {
            if (_flightSearchValidator.IsValid(searchFlight)) return BadRequest();
            
            return Ok(_customerService.FindFlights(searchFlight));
        }

        [HttpGet]
        [Route("flights/{id:int}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _customerService.GetFullFlight(id);

            if (flight == null) return BadRequest();

            return Ok(_mapper.Map<FlightSearchResult>(flight));
        }
    }
}