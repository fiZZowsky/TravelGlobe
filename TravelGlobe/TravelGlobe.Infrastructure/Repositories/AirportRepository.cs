using Microsoft.EntityFrameworkCore;
using TravelGlobe.Domain.Entities;
using TravelGlobe.Domain.Interfaces;
using TravelGlobe.Infrastructure.Persistance;

namespace TravelGlobe.Infrastructure.Repositories;

public class AirportRepository : IAirportRepository
{
    private readonly TravelGlobeDbContext _db;
    public AirportRepository(TravelGlobeDbContext db) => _db = db;

    public async Task<Airport> GetByIdAsync(int id) =>
        await _db.Airports.FindAsync(id);

    public async Task<Airport> GetByIataAsync(string iata) =>
        await _db.Airports.FirstOrDefaultAsync(a => a.Code.Iata == iata);

    public async Task<IReadOnlyList<Airport>> ListByCountryAsync(string countryCode) =>
        await _db.Airports
                 .AsNoTracking()
                 .Where(a => a.CountryCode == countryCode)
                 .ToListAsync();

    public async Task<IReadOnlyList<Airport>> SearchByNameAsync(string query)
    {
        var normalized = query.Trim().ToLower();
        var pattern = $"%{normalized}%";

        return await _db.Airports
            .AsNoTracking()
            .Where(a =>
                EF.Functions.Like(a.Name.ToLower(), pattern)
                || EF.Functions.Like(a.City.ToLower(), pattern)
            )
            .OrderBy(a => a.Name)
            .Take(5)
            .ToListAsync();
    }
}
