using Android.Views;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using static Android.Views.ViewGroup;

namespace TravelGlobe.MobileApp;

public class FloatingNavAppearanceTracker : IShellBottomNavViewAppearanceTracker
{
    readonly IShellContext _context;

    public FloatingNavAppearanceTracker(IShellContext context)
    {
        _context = context;
    }

    public void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
    {
        var context = bottomView.Context;
        int margin = (int)(16 * context.Resources.DisplayMetrics.Density);
        if (bottomView.LayoutParameters is MarginLayoutParams lp)
        {
            lp.BottomMargin = margin;
            lp.LeftMargin = margin;
            lp.RightMargin = margin;
            bottomView.LayoutParameters = lp;
        }
        bottomView.SetBackgroundResource(Resource.Drawable.rounded_navbar);
        bottomView.SetClipToOutline(true);
    }

    public void ResetAppearance(BottomNavigationView bottomView)
    {
        // default reset behavior
    }

    public void Dispose() { }
}