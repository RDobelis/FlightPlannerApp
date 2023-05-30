using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IFlightPlannerDbContext _context;

        public PageResult FindFlights(FlightSearch searchFlight)
        {
            var result = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .Where(f => f.From.AirportCode == searchFlight.From &&
                            f.To.AirportCode == searchFlight.To &&
                            f.DepartureTime.StartsWith(searchFlight.DepartureDate))
                .ToList();

            var response = new PageResult
            {
                Page = 0,
                TotalItems = result.Count,
                Items = result
            };

            return response;
        }

        public Flight GetFullFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .FirstOrDefault(f => f.Id == id);
            return flight;
        }

        public bool HasInvalidDetails(FlightSearch searchFlight)
        {
            return searchFlight.From == null ||
                   searchFlight.To == null ||
                   searchFlight.DepartureDate == null;
        }

        public bool MatchingAirport(FlightSearch searchFlight)
        {
            return searchFlight.From == searchFlight.To;
        }
    }
}
