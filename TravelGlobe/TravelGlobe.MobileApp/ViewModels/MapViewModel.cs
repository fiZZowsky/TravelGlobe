using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using TravelGlobe.Application;
using TravelGlobe.Application.Services;
using TravelGlobe.Domain.Interfaces;
using TravelGlobe.MobileApp.Views;

namespace TravelGlobe.MobileApp.ViewModels;

public class MapViewModel : BindableObject
{
    private readonly ITripService _tripService;
    private readonly IAirportRepository _airportRepo;

    public ObservableCollection<TripDTO> VisitedTrips { get; } = new();
    public ObservableCollection<string> VisitedCountryCodes { get; } = new();
    public ObservableCollection<AirportInfo> Airports { get; } = new();

    public ICommand AddTripCommand { get; }

    public MapViewModel(ITripService tripService, IAirportRepository airportRepo)
    {
        _tripService = tripService;
        _airportRepo = airportRepo;
        AddTripCommand = new Command(OnAddTrip);
        _ = LoadData();
    }

    public string GetMapDataJson() => JsonSerializer.Serialize(new
    {
        countries = VisitedCountryCodes,
        airports = Airports
    });

    private async Task LoadData()
    {
        var trips = await _tripService.ListTripsByUserAsync(1);
        VisitedTrips.Clear();
        VisitedCountryCodes.Clear();
        Airports.Clear();
        var airportIds = new HashSet<int>();
        foreach (var t in trips)
        {
            VisitedTrips.Add(t);
            if (!VisitedCountryCodes.Contains(t.CountryCode))
                VisitedCountryCodes.Add(t.CountryCode);
            await AddAirport(t.DepartureAirportId, airportIds);
            await AddAirport(t.ArrivalAirportId, airportIds);
            await AddAirport(t.ReturnDepartureAirportId, airportIds);
            await AddAirport(t.ReturnArrivalAirportId, airportIds);
        }
    }

    private async Task AddAirport(int id, HashSet<int> visited)
    {
        if (!visited.Add(id))
            return;
        var a = await _airportRepo.GetByIdAsync(id);
        if (a != null)
            Airports.Add(new AirportInfo { Name = a.Name, Lat = a.Location.Latitude, Lon = a.Location.Longitude });
    }

    private async void OnAddTrip() => await Shell.Current.GoToAsync(nameof(TripFormPage));
}

public class AirportInfo
{
    public string Name { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}