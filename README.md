# FlightPlanner

FlightPlanner is a web application that allows customers to search for flights and administrators to manage flights. The application provides APIs for searching flights and airports, adding and deleting flights, and clearing the list of flights.

## Getting Started

To get started with FlightPlanner, you'll need to have the following installed on your system:

- [.NET Core SDK](https://dotnet.microsoft.com/download)

Once you've installed the required software, you can run FlightPlanner by following these steps:

1. Clone the repository to your local machine.
2. Navigate to the `FlightPlanner` directory.
3. Run `dotnet build` to build the application.
4. Run `dotnet run` to start the application.

By default, the application runs on `http://localhost:5000`.

## API Endpoints

The FlightPlanner application provides the following API endpoints:

### Customer API

- `GET /api/airports?search={search}`: Returns a list of airports that match the specified search term.
- `POST /api/flights/search`: Searches for flights that match the specified criteria.
- `GET /api/flights/{id}`: Returns the flight with the specified ID.

### Admin API

- `GET /admin-api/flights/{id}`: Returns the flight with the specified ID.
- `PUT /admin-api/flights`: Adds a new flight.
- `DELETE /admin-api/flights/{id}`: Deletes the flight with the specified ID.

### Testing API

- `POST /testing-api/clear`: Clears the list of flights.

## Data Models

The FlightPlanner application uses the following data models:

- `Airport`: Represents an airport.
- `Flight`: Represents a flight.
- `FlightSearch`: Represents a flight search request.
- `AddFlightRequest`: Represents a request to add a new flight.
- `PageResult`: Represents a page of search results.

## Controllers

The FlightPlanner application uses the following controllers:

- `CustomerApiController`: Handles requests made by customers.
- `AdminApiController`: Handles requests made by administrators.
- `TestingApiController`: Handles requests related to testing.

## Storage

The FlightPlanner application uses a simple in-memory storage mechanism to store flights. The `FlightStorage` class provides methods for adding, deleting, and searching for flights.

## Validation

The FlightPlanner application uses validation to ensure that input data is valid. The `ValidateSearch` class provides methods for validating flight search requests, and the `ValidateFlight` class provides methods for validating requests to add new flights.

## Authentication

The FlightPlanner application uses token-based authentication to secure the admin API endpoints. Admin users must include a valid access token in the `Authorization` header of their requests.
