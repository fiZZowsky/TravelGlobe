using System.Text.Json;
using Microsoft.Maui.Controls;
using TravelGlobe.MobileApp.ViewModels;

namespace TravelGlobe.MobileApp.Views;

public partial class MapPage : ContentPage
{
    public MapPage(MapViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        MapWebView.Navigated += OnMapNavigated;
    }

    private async void OnMapNavigated(object sender, WebNavigatedEventArgs e)
    {
        if (BindingContext is MapViewModel vm)
        {
            var json = vm.GetMapDataJson();
            var script = $"(function(){{var d={json};updateVisitedCountries(d.countries);updateAirports(d.airports);}})();";
            await MapWebView.EvaluateJavaScriptAsync(script);
        }
    }
}