namespace TravelGlobe.MobileApp.Views;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

public partial class SetupPage : ContentPage
{
    public SetupPage()
    {
        InitializeComponent();
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        var nickname = nicknameEntry.Text?.Trim();
        var country = countryEntry.Text?.Trim();
        if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(country))
        {
            await DisplayAlert("Błąd", "Podaj pseudonim i kraj.", "OK");
            return;
        }

        Preferences.Set("UserNickname", nickname);
        Preferences.Set("UserCountry", country);

        Application.Current!.MainPage = new AppShell();
    }
}