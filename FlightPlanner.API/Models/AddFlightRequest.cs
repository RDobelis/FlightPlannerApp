using FlightPlanner.Core.Models;

namespace FlightPlanner.API.Models
{
    public class AddFlightRequest : Entity
    {
        public AddAirportRequest From { get; set; }
        public AddAirportRequest To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
