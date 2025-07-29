namespace TravelGlobe.MobileApp.Views;

using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using TravelGlobe.Domain.Entities;
using TravelGlobe.Domain.Interfaces;

public partial class SetupPage : ContentPage
{
    private readonly ICountryRepository _countryRepo;

    public SetupPage(ICountryRepository countryRepo)
    {
        InitializeComponent();
        _countryRepo = countryRepo;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var countries = await _countryRepo.ListAllAsync();
        countryPicker.ItemsSource = countries;
    }

    private async void OnStartClicked(object sender, EventArgs e)
    {
        var nickname = nicknameEntry.Text?.Trim();
        var country = countryPicker.SelectedItem as Country;
        if (string.IsNullOrEmpty(nickname) || country == null)
        {
            await DisplayAlert("Błąd", "Podaj pseudonim i kraj.", "OK");
            return;
        }

        Preferences.Set("UserNickname", nickname);
        Preferences.Set("UserCountry", country.Name);

        Application.Current!.MainPage = new AppShell();
    }
}