using TravelGlobe.Domain.Entities;

namespace TravelGlobe.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task AddAsync(User user);
}