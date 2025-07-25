using Microsoft.EntityFrameworkCore;
using TravelGlobe.Domain.Entities;
using TravelGlobe.Domain.Interfaces;
using TravelGlobe.Infrastructure.Persistance;

namespace TravelGlobe.Infrastructure.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TravelGlobeDbContext _db;

        public TripRepository(TravelGlobeDbContext db) => _db = db;

        public async Task<Trip?> GetByIdAsync(int id) =>
            await _db.Trips.FindAsync(id);

        public async Task<IReadOnlyList<Trip>> ListByUserAsync(int userId) =>
            await _db.Trips
                     .AsNoTracking()
                     .Where(t => t.UserId == userId)
                     .ToListAsync();

        public async Task AddAsync(Trip trip)
        {
            await _db.Trips.AddAsync(trip);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveAsync(Trip trip)
        {
            _db.Trips.Remove(trip);
            await _db.SaveChangesAsync();
        }
    }
}
