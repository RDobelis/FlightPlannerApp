using System.Linq;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class CleanupService : ICleanupService
    {
        private readonly IFlightPlannerDbContext _context;

        public CleanupService(IFlightPlannerDbContext context)
        {
            _context = context;
        }

        public void CleanupDatabase()
        {
            var flights = _context.Flights.ToList();
            _context.Flights.RemoveRange(flights);

            var airports = _context.Airports.ToList();
            _context.Airports.RemoveRange(airports);

            _context.SaveChanges();
        }
    }
}
