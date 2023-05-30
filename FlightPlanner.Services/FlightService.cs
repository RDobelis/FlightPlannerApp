using System;
using System.Linq;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight GetFullFlight(int id)
        {
            return _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public bool FlightExists(Flight flight)
        {
            return _context.Flights.Any(f =>
                f.ArrivalTime == flight.ArrivalTime &&
                f.Carrier == flight.Carrier &&
                f.DepartureTime == flight.DepartureTime &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.From.City == flight.From.City &&
                f.From.Country == flight.From.Country &&
                f.To.AirportCode == flight.To.AirportCode &&
                f.To.City == flight.To.City &&
                f.To.Country == flight.To.Country
            );
        }

        public bool HasInvalidFlightDetails(Flight flight)
        {
            if (flight == null || flight.From == null || flight.To == null) return true;

            return string.IsNullOrWhiteSpace(flight.From.AirportCode) ||
                   string.IsNullOrWhiteSpace(flight.To.AirportCode) ||
                   string.IsNullOrWhiteSpace(flight.From.Country) ||
                   string.IsNullOrWhiteSpace(flight.To.Country) ||
                   string.IsNullOrWhiteSpace(flight.From.City) ||
                   string.IsNullOrWhiteSpace(flight.To.City) ||
                   string.IsNullOrWhiteSpace(flight.Carrier) ||
                   string.IsNullOrWhiteSpace(flight.DepartureTime) ||
                   string.IsNullOrWhiteSpace(flight.ArrivalTime);
        }

        public bool HasInvalidFlightTime(Flight flight)
        {
            if (DateTime.TryParse(flight.ArrivalTime, out var arrivalTime) &&
                DateTime.TryParse(flight.DepartureTime, out var departureTime))
                return arrivalTime <= departureTime;

            return false;
        }

        public bool HasInvalidAirport(Flight flight)
        {
            return flight.From.AirportCode.Trim().ToUpper() == flight.To.AirportCode.Trim().ToUpper();
        }
    }
}
