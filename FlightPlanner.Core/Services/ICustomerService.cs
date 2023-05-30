using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface ICustomerService
    {
        PageResult FindFlights(FlightSearch searchFlight);
        Flight GetFullFlight(int id);
        bool HasInvalidDetails(FlightSearch searchFlight);
        bool MatchingAirport(FlightSearch searchFlight);
    }
}
