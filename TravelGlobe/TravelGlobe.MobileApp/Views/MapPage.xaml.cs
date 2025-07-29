using System.Text.Json;
using System.Linq;
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
        viewModel.PropertyChanged += ViewModelOnPropertyChanged;
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

    private async void ViewModelOnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(MapViewModel.SelectedDeparture) ||
            e.PropertyName == nameof(MapViewModel.SelectedArrival) ||
            e.PropertyName == nameof(MapViewModel.SelectedReturnDeparture) ||
            e.PropertyName == nameof(MapViewModel.SelectedReturnArrival))
        {
            if (BindingContext is MapViewModel vm)
            {
                var json = JsonSerializer.Serialize(new
                {
                    departure = vm.SelectedDeparture,
                    arrival = vm.SelectedArrival,
                    returnDeparture = vm.SelectedReturnDeparture,
                    returnArrival = vm.SelectedReturnArrival
                });
                var script = $"(function(){{var d={json};updateSelection(d);}})();";
                await MapWebView.EvaluateJavaScriptAsync(script);
            }
        }
    }

    private void OnDepartureTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is MapViewModel vm)
            vm.SearchDepartureCommand.Execute(e.NewTextValue);
    }

    private void OnArrivalTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is MapViewModel vm)
            vm.SearchArrivalCommand.Execute(e.NewTextValue);
    }

    private void OnReturnDepartureTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is MapViewModel vm)
            vm.SearchReturnDepartureCommand.Execute(e.NewTextValue);
    }

    private void OnReturnArrivalTextChanged(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is MapViewModel vm)
            vm.SearchReturnArrivalCommand.Execute(e.NewTextValue);
    }

    private void OnDepartureSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is AirportInfo info)
        {
            dep.Text = info.Name;
            if (BindingContext is MapViewModel vm)
                vm.DepartureResults.Clear();
        }
    }

    private void OnArrivalSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is AirportInfo info)
        {
            arr.Text = info.Name;
            if (BindingContext is MapViewModel vm)
                vm.ArrivalResults.Clear();
        }
    }

    private void OnReturnDepartureSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is AirportInfo info)
        {
            retDep.Text = info.Name;
            if (BindingContext is MapViewModel vm)
                vm.ReturnDepartureResults.Clear();
        }
    }

    private void OnReturnArrivalSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is AirportInfo info)
        {
            retArr.Text = info.Name;
            if (BindingContext is MapViewModel vm)
                vm.ReturnArrivalResults.Clear();
        }
    }
}