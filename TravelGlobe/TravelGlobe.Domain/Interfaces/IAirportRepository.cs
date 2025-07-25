using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Domain.Interfaces;

public interface IAirportRepository
{
    Task<Airport?> GetByIdAsync(int id);
    Task<Airport?> GetByIataAsync(string iataCode);
    Task<IReadOnlyList<Airport>> ListByCountryAsync(string countryCode);
}
