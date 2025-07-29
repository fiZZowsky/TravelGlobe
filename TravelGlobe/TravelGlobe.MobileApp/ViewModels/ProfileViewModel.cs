using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using System.Windows.Input;
using TravelGlobe.Application.Services;

namespace TravelGlobe.MobileApp.ViewModels
{
    public class ProfileViewModel : BindableObject
    {
        private readonly ITripService _tripService;
        public string Nickname { get; }
        public string Country { get; }
        public int VisitedCount { get; private set; }
        public double VisitedPercentage { get; private set; }
        public int TotalDistanceKm { get; private set; }
        public ICommand RefreshCommand { get; }

        public ProfileViewModel(ITripService tripService)
        {
            _tripService = tripService;
            Nickname = Preferences.Get("UserNickname", string.Empty);
            Country = Preferences.Get("UserCountry", string.Empty);
            RefreshCommand = new Command(async () => await LoadStats());
            _ = LoadStats();
        }

        private async Task LoadStats()
        {
            var stats = await _tripService.GetStatisticsAsync(1);
            VisitedCount = stats.VisitedCount;
            VisitedPercentage = stats.VisitedPercentage;
            TotalDistanceKm = stats.TotalDistanceKm;
            OnPropertyChanged(string.Empty);
        }
    }
}
