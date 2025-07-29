namespace TravelGlobe.MobileApp
{
    public partial class App : Microsoft.Maui.Controls.Application
    {
        public App()
        {
            InitializeComponent();

            var nickname = Microsoft.Maui.Storage.Preferences.Get("UserNickname", null);
            var country = Microsoft.Maui.Storage.Preferences.Get("UserCountry", null);

            if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(country))
                MainPage = new Views.SetupPage();
            else
                MainPage = new AppShell();
        }
    }
}