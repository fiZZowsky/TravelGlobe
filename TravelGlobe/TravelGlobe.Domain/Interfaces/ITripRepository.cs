using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Domain.Interfaces;

public interface ITripRepository
{
    Task<Trip?> GetByIdAsync(int id);
    Task<IReadOnlyList<Trip>> ListByUserAsync(int userId);
    Task AddAsync(Trip trip);
    Task RemoveAsync(Trip trip);
}
