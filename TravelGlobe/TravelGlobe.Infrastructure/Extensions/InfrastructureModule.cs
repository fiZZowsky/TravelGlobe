using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelGlobe.Domain.Interfaces;
using TravelGlobe.Infrastructure.Persistance;
using TravelGlobe.Infrastructure.Repositories;

namespace TravelGlobe.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        // 1) DbContext z SQLite
        var connString = config.GetConnectionString("TravelGlobeSqlite")
                         ?? "Data Source=travelglobe.db";
        services.AddDbContext<TravelGlobeDbContext>(opts =>
            opts.UseSqlite(connString));

        // 2) Repozytoria
        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IAirportRepository, AirportRepository>();

        return services;
    }
}
