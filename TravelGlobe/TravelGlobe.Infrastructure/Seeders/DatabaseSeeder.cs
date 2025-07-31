using System.Linq;
using System.Reflection;
using TravelGlobe.Database;
using TravelGlobe.Domain.Entities;
using TravelGlobe.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace TravelGlobe.Infrastructure.Seeders;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(TravelGlobeDbContext db)
    {
        var assembly = typeof(DatabaseMarker).Assembly;

        if (!await db.Countries.AnyAsync())
        {
            await ExecuteSqlFromResourceAsync(db, assembly, "data_countries.sql");
        }

        if (!await db.Airports.AnyAsync())
        {
            await ExecuteSqlFromResourceAsync(db, assembly, "data_airport.sql");
        }
    }

    private static async Task ExecuteSqlFromResourceAsync(TravelGlobeDbContext db, Assembly assembly, string fileName)
    {
        var resourceName = assembly.GetManifestResourceNames()
            .FirstOrDefault(n => n.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

        if (resourceName is null)
            return;

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream is null)
            return;

        using var reader = new StreamReader(stream);
        var sql = await reader.ReadToEndAsync();
        await db.Database.ExecuteSqlRawAsync(sql);
    }
}