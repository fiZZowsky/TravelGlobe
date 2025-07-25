using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelGlobe.Application.Services;
using TravelGlobe.Infrastructure;
using TravelGlobe.Domain.Factories;
using TravelGlobe.Domain.ValueObjects;

namespace TravelGlobe.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddInfrastructure(config);
        services.AddScoped<IDistanceCalculator, DistanceCalculator>();
        services.AddScoped<ITripFactory, TripFactory>();
        services.AddScoped<ITripService, TripService>();

        return services;
    }
}