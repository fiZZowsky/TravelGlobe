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
#if ANDROID
        GlobeWebView.Source = "https://appassets.androidplatform.net/assets/globe.html";
#else
        GlobeWebView.Source = "globe.html";
#endif
    }
}
