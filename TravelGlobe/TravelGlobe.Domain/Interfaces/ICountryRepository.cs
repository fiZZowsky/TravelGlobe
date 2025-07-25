using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Domain.Interfaces;

public interface ICountryRepository
{
    Task<Country?> GetByCodeAsync(string countryCode);
    Task<IReadOnlyList<Country>> ListAllAsync();
}
