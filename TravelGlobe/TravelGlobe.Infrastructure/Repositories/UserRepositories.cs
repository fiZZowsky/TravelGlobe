using Microsoft.EntityFrameworkCore;
using TravelGlobe.Domain.Entities;
using TravelGlobe.Domain.Interfaces;
using TravelGlobe.Infrastructure.Persistance;

namespace TravelGlobe.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TravelGlobeDbContext _db;
    public UserRepository(TravelGlobeDbContext db) => _db = db;

    public async Task<User?> GetByIdAsync(int id) => await _db.Users.FindAsync(id);

    public async Task AddAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }
}