using Android.Views;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Platform;
using static Android.Views.ViewGroup;

namespace TravelGlobe.MobileApp;

public partial class CustomShellHandler
{
    partial void CustomizeTabBar()
    {
        var bottomNav = FindBottomNavView(PlatformView);
        if (bottomNav != null)
        {
            var context = bottomNav.Context;
            int margin = (int)(16 * context.Resources.DisplayMetrics.Density);
            if (bottomNav.LayoutParameters is MarginLayoutParams lp)
            {
                lp.BottomMargin = margin;
                lp.LeftMargin = margin;
                lp.RightMargin = margin;
                bottomNav.LayoutParameters = lp;
            }
            bottomNav.SetBackgroundResource(Resource.Drawable.rounded_navbar);
            bottomNav.SetPadding(0, 0, 0, 0);
            bottomNav.SetClipToOutline(true);
        }
    }

    BottomNavigationView? FindBottomNavView(View view)
    {
        if (view is BottomNavigationView bnv)
            return bnv;
        if (view is ViewGroup vg)
        {
            for (int i = 0; i < vg.ChildCount; i++)
            {
                var result = FindBottomNavView(vg.GetChildAt(i));
                if (result != null)
                    return result;
            }
        }
        return null;
    }
}