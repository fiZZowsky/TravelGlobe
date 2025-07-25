using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelGlobe.Application.Services;

namespace TravelGlobe.MobileApp.ViewModels
{
    public class ProfileViewModel : BindableObject
    {
        private readonly ITripService _tripService;
        public int VisitedCount { get; private set; }
        public double VisitedPercentage { get; private set; }
        public int TotalDistanceKm { get; private set; }
        public ICommand RefreshCommand { get; }

        public ProfileViewModel(ITripService tripService)
        {
            _tripService = tripService;
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
