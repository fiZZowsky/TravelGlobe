using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TravelGlobe.Domain.Entities;
using TravelGlobe.Infrastructure.Persistance;

namespace TravelGlobe.Infrastructure;

public class DesignTimeDbContextFactory
        : IDesignTimeDbContextFactory<TravelGlobeDbContext>
{
    public TravelGlobeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TravelGlobeDbContext>();

        var dbPath = Path.Combine("C:\\Users\\Dell\\Desktop\\TravelGlobe",
            "travelglobe.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
        return new TravelGlobeDbContext(optionsBuilder.Options);
    }
}
