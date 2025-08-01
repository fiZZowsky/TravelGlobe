using Microsoft.Maui.Handlers;
#if ANDROID
using Android.Views;
using Android.Widget; // dla ImageView i ViewGroup
using SearchView = AndroidX.AppCompat.Widget.SearchView; // jasno wskazujemy, którego SearchView używamy
#elif IOS || MACCATALYST
using Microsoft.Maui.Platform;
#elif WINDOWS
using Microsoft.UI.Xaml.Controls;
#endif

namespace TravelGlobe.MobileApp;

public class NoIconSearchBarHandler : SearchBarHandler
{
#if ANDROID
    protected override void ConnectHandler(SearchView platformView)
    {
        base.ConnectHandler(platformView);

        var searchIconId = platformView.Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
        var searchIcon = platformView.FindViewById<ImageView>(searchIconId);
        if (searchIcon != null)
        {
            searchIcon.LayoutParameters = new ViewGroup.LayoutParams(0, 0);
            searchIcon.RequestLayout();
        }
    }
#elif IOS || MACCATALYST
    protected override void ConnectHandler(MauiSearchBar platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SearchTextField.LeftView = null;
    }
#elif WINDOWS
    protected override void ConnectHandler(AutoSuggestBox platformView)
    {
        base.ConnectHandler(platformView);
        platformView.QueryIcon = null;
    }
#endif
}
