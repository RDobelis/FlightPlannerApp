using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFullFlight(int id);
        public bool FlightExists(Flight flight);
        public bool HasInvalidFlightDetails(Flight flight);
        public bool HasInvalidFlightTime(Flight flight);
        public bool HasInvalidAirport(Flight flight);
    }
}
