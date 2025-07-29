using Microsoft.EntityFrameworkCore;
using TravelGlobe.Domain.Entities;
using TravelGlobe.Domain.Interfaces;
using TravelGlobe.Infrastructure.Persistance;

namespace TravelGlobe.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly TravelGlobeDbContext _db;
        public CountryRepository(TravelGlobeDbContext db) => _db = db;

        public async Task<Country?> GetByCodeAsync(string code) =>
            await _db.Countries.FindAsync(code);

        public async Task<List<Country>> ListAllAsync() =>
            await _db.Countries
                     .AsNoTracking()
                     .ToListAsync();
    }
}