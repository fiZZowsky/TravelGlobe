using System.Collections.ObjectModel;
using System.Windows.Input;
using TravelGlobe.Application;
using TravelGlobe.Application.Services;
using TravelGlobe.MobileApp.Views;

namespace TravelGlobe.MobileApp.ViewModels
{
    public class MapViewModel : BindableObject
    {
        private readonly ITripService _tripService;
        public ObservableCollection<TripDTO> VisitedCountries { get; } = new();
        public ICommand AddTripCommand { get; }

        public MapViewModel(ITripService tripService)
        {
            _tripService = tripService;
            AddTripCommand = new Command(OnAddTrip);
            _ = LoadData();
        }

        private async Task LoadData()
        {
            var trips = await _tripService.ListTripsByUserAsync(1);
            VisitedCountries.Clear();
            foreach (var t in trips)
                VisitedCountries.Add(t);
        }

        private async void OnAddTrip() => await Shell.Current.GoToAsync(nameof(TripFormPage));
    }
}
