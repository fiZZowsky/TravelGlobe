using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using TravelGlobe.Application;
using TravelGlobe.Application.Services;
using TravelGlobe.Domain.Interfaces;

namespace TravelGlobe.MobileApp.ViewModels;

public class MapViewModel : BindableObject
{
    private readonly ITripService _tripService;
    private readonly IAirportRepository _airportRepo;

    public ObservableCollection<TripDTO> VisitedTrips { get; } = new();
    public ObservableCollection<string> VisitedCountryCodes { get; } = new();
    public ObservableCollection<AirportInfo> Airports { get; } = new();

    public ObservableCollection<AirportInfo> DepartureResults { get; } = new();
    public ObservableCollection<AirportInfo> ArrivalResults { get; } = new();
    public ObservableCollection<AirportInfo> ReturnDepartureResults { get; } = new();
    public ObservableCollection<AirportInfo> ReturnArrivalResults { get; } = new();

    private AirportInfo _selectedDeparture;
    public AirportInfo SelectedDeparture
    {
        get => _selectedDeparture;
        set
        {
            _selectedDeparture = value;
            OnPropertyChanged();
            UpdateSelection();
        }
    }

    private AirportInfo _selectedArrival;
    public AirportInfo SelectedArrival
    {
        get => _selectedArrival;
        set
        {
            _selectedArrival = value;
            OnPropertyChanged();
            if (SameReturn)
            {
                SelectedReturnDeparture = _selectedArrival;
                SelectedReturnArrival = _selectedDeparture;
            }
            UpdateSelection();
        }
    }

    private AirportInfo _selectedReturnDeparture;
    public AirportInfo SelectedReturnDeparture
    {
        get => _selectedReturnDeparture;
        set
        {
            _selectedReturnDeparture = value;
            OnPropertyChanged();
            UpdateSelection();
        }
    }

    private AirportInfo _selectedReturnArrival;
    public AirportInfo SelectedReturnArrival
    {
        get => _selectedReturnArrival;
        set
        {
            _selectedReturnArrival = value;
            OnPropertyChanged();
            UpdateSelection();
        }
    }

    private bool _sameReturn;
    public bool SameReturn
    {
        get => _sameReturn;
        set
        {
            if (_sameReturn == value)
                return;

            _sameReturn = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ReturnFieldsVisible));

            if (_sameReturn && SelectedDeparture != null && SelectedArrival != null)
            {
                SelectedReturnDeparture = SelectedArrival;
                SelectedReturnArrival = SelectedDeparture;
            }
        }
    }

    private bool _oneWay;
    public bool OneWay
    {
        get => _oneWay;
        set
        {
            if (_oneWay == value)
                return;

            _oneWay = value;

            if (_oneWay)
                SameReturn = false;

            OnPropertyChanged();
            OnPropertyChanged(nameof(SameReturnEnabled));
            OnPropertyChanged(nameof(ReturnFieldsVisible));
        }
    }

    public bool SameReturnEnabled => !OneWay;
    public bool ReturnFieldsVisible => !OneWay && !SameReturn;

    public ICommand SearchDepartureCommand { get; }
    public ICommand SearchArrivalCommand { get; }
    public ICommand SearchReturnDepartureCommand { get; }
    public ICommand SearchReturnArrivalCommand { get; }

    public ICommand SaveTripCommand { get; }

    public MapViewModel(ITripService tripService, IAirportRepository airportRepo)
    {
        _tripService = tripService;
        _airportRepo = airportRepo;

        SearchDepartureCommand = new Command<string>(async q => await SearchAsync(q, DepartureResults, nameof(DepartureResults)));
        SearchArrivalCommand = new Command<string>(async q => await SearchAsync(q, ArrivalResults, nameof(ArrivalResults)));
        SearchReturnDepartureCommand = new Command<string>(async q => await SearchAsync(q, ReturnDepartureResults, nameof(ReturnDepartureResults)));
        SearchReturnArrivalCommand = new Command<string>(async q => await SearchAsync(q, ReturnArrivalResults, nameof(ReturnArrivalResults)));

        SaveTripCommand = new Command(async () => await OnSave());

        _ = LoadData();
    }

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public string GetMapDataJson() => JsonSerializer.Serialize(new
    {
        countries = VisitedCountryCodes,
        airports = Airports
    }, _jsonOptions);

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
            Airports.Add(new AirportInfo
            {
                Id = a.Id,
                Name = a.Name,
                CountryCode = a.CountryCode,
                Lat = a.Location.Latitude,
                Lon = a.Location.Longitude
            });
    }

    private async Task SearchAsync(string query, ObservableCollection<AirportInfo> target, string propertyName)
    {
        target.Clear();
        if (string.IsNullOrWhiteSpace(query))
        {
            OnPropertyChanged(propertyName);
            return;
        }
        var results = await _airportRepo.SearchByNameAsync(query);
        foreach (var a in results)
        {
            target.Add(new AirportInfo
            {
                Id = a.Id,
                Name = a.Name,
                CountryCode = a.CountryCode,
                Lat = a.Location.Latitude,
                Lon = a.Location.Longitude
            });
        }
        OnPropertyChanged(propertyName);
    }

    public void ClearResults(ObservableCollection<AirportInfo> target, string propertyName)
    {
        target.Clear();
        OnPropertyChanged(propertyName);
    }

    private void UpdateSelection()
    {
        OnPropertyChanged(nameof(SelectedDeparture));
    }

    private async Task OnSave()
    {
        if (SelectedDeparture == null || SelectedArrival == null)
            return;

        var dto = new TripDTO
        {
            CountryCode = SelectedArrival.CountryCode,
            DepartureAirportId = SelectedDeparture.Id,
            ArrivalAirportId = SelectedArrival.Id,
            ReturnDepartureAirportId = OneWay ? SelectedArrival.Id : (SelectedReturnDeparture?.Id ?? SelectedArrival.Id),
            ReturnArrivalAirportId = OneWay ? SelectedDeparture.Id : (SelectedReturnArrival?.Id ?? SelectedDeparture.Id)
        };

        await _tripService.AddTripAsync(dto);
        await LoadData();
    }
}

public class AirportInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
}