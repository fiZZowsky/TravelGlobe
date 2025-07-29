using TravelGlobe.Domain.Entities;
using TravelGlobe.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace TravelGlobe.Infrastructure.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(TravelGlobeDbContext db)
    {
        if (!await db.Airports.AnyAsync())
        {
            var script = Path.Combine(AppContext.BaseDirectory, "Scripts", "data_airport.sql");
            if (File.Exists(script))
            {
                var sql = await File.ReadAllTextAsync(script);
                await db.Database.ExecuteSqlRawAsync(sql);
            }
        }
    }
}
