using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TravelGlobe.Application.Services;
using TravelGlobe.Infrastructure;

namespace TravelGlobe.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddInfrastructure(config);
        services.AddScoped<ITripService, TripService>();

        return services;
    }
}