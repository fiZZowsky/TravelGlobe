using Microsoft.Maui.Controls;
using TravelGlobe.MobileApp.ViewModels;

namespace TravelGlobe.MobileApp.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage(ProfileViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
