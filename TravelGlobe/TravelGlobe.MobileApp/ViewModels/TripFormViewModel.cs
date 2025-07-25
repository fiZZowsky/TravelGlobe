using System.Windows.Input;
using TravelGlobe.Application;
using TravelGlobe.Application.Services;

namespace TravelGlobe.MobileApp.ViewModels
{
    public class TripFormViewModel : BindableObject
    {
        private readonly ITripService _tripService;
        public string CountryCode { get; set; }
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public int ReturnDepartureAirportId { get; set; }
        public int ReturnArrivalAirportId { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public TripFormViewModel(ITripService tripService)
        {
            _tripService = tripService;
            SaveCommand = new Command(async () => await OnSave());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
        }

        private async Task OnSave()
        {
            var dto = new TripDTO
            {
                CountryCode = CountryCode,
                DepartureAirportId = DepartureAirportId,
                ArrivalAirportId = ArrivalAirportId,
                ReturnDepartureAirportId = ReturnDepartureAirportId,
                ReturnArrivalAirportId = ReturnArrivalAirportId
            };
            await _tripService.AddTripAsync(dto);
            await Shell.Current.GoToAsync("..");
        }
    }
}
