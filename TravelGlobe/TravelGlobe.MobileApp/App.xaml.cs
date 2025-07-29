using Microsoft.Extensions.DependencyInjection;

namespace TravelGlobe.MobileApp
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        private readonly IServiceProvider _services;

        public App(IServiceProvider services)
        {
            InitializeComponent();
            _services = services;

            var nickname = Microsoft.Maui.Storage.Preferences.Get("UserNickname", null);
            var country = Microsoft.Maui.Storage.Preferences.Get("UserCountry", null);

            if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(country))
                MainPage = _services.GetRequiredService<Views.SetupPage>();
            else
                MainPage = new AppShell();
        }
    }
}