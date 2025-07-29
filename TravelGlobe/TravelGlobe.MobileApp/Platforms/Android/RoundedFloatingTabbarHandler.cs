using Android.Graphics.Drawables;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using static Android.Views.ViewGroup;

namespace TravelGlobe.MobileApp
{
    internal class RoundedFloatingTabbarHandler : ShellRenderer
    {
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new RoundedFloatingBottomNavViewAppearenceTracker(this, shellItem);
        }
    }

    internal class RoundedFloatingBottomNavViewAppearenceTracker : ShellBottomNavViewAppearanceTracker
    {
        public RoundedFloatingBottomNavViewAppearenceTracker(IShellContext shellContext, ShellItem shellItem)
             : base(shellContext, shellItem)
        {
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearence)
        {
            base.SetAppearance(bottomView, appearence);

            var tabbarDrawable = new GradientDrawable();
            tabbarDrawable.SetCornerRadius(50);
            tabbarDrawable.SetColor(appearence.EffectiveTabBarBackgroundColor.ToPlatform());

            bottomView.SetBackground(tabbarDrawable);

            LayoutParams layoutParameters = bottomView.LayoutParameters;
            if (layoutParameters is MarginLayoutParams marginLayoutParams)
            {
                marginLayoutParams.SetMargins(50, 0, 50, 50);
                bottomView.LayoutParameters = marginLayoutParams;
            }
        }
    }
}
