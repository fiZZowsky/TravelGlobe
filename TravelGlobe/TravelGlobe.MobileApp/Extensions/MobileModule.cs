using TravelGlobe.MobileApp.ViewModels;
using TravelGlobe.MobileApp.Views;

namespace TravelGlobe.MobileApp;

public static class MobileModule
{
    public static IServiceCollection AddMobile(this IServiceCollection services)
    {
        services.AddTransient<MapViewModel>();
        services.AddTransient<ProfileViewModel>();

        services.AddTransient<HomePage>();
        services.AddTransient<MapPage>();
        services.AddTransient<ProfilePage>();
        services.AddTransient<SetupPage>();

        return services;
    }
}
