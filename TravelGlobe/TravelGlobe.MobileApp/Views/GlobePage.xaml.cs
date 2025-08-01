namespace TravelGlobe.MobileApp.Views;

public partial class GlobePage : ContentPage
{
    public GlobePage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        GlobeWebView.Source = "globe.html";
    }
}
