using TravelGlobe.MobileApp.ViewModels;
using TravelGlobe.MobileApp.Views;

namespace TravelGlobe.MobileApp;

public static class MobileModule
{
    public static IServiceCollection AddMobile(this IServiceCollection services)
    {
        services.AddTransient<MapViewModel>();
        services.AddTransient<TripFormViewModel>();
        services.AddTransient<ProfileViewModel>();

        services.AddTransient<MapPage>();
        services.AddTransient<TripFormPage>();
        services.AddTransient<ProfilePage>();

        Routing.RegisterRoute(nameof(TripFormPage), typeof(TripFormPage));

        return services;
    }
}
