using Microsoft.Maui.Controls;
using TravelGlobe.MobileApp.ViewModels;

namespace TravelGlobe.MobileApp.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage(MapViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}