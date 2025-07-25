using Microsoft.Maui.Controls;
using TravelGlobe.MobileApp.ViewModels;

namespace TravelGlobe.MobileApp.Views
{
    public partial class TripFormPage : ContentPage
    {
        public TripFormPage(TripFormViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
