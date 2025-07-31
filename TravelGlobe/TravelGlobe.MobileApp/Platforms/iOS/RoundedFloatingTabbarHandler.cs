using CoreAnimation;
using CoreGraphics;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using UIKit;

namespace TravelGlobe.MobileApp
{
    internal class RoundedFloatingTabbarHandler : ShellRenderer
    {
        protected override IShellTabBarAppearanceTracker CreateTabBarAppearanceTracker()
        {
            return new RoundedFloatingShellTabBarAppearanceTracker();
        }
    }

    internal class RoundedFloatingShellTabBarAppearanceTracker : ShellTabBarAppearanceTracker
    {
        public override void UpdateLayout(UITabBarController controller)
        {
            base.UpdateLayout(controller);

            AddMarginsToTheTabbar(controller);

            var shapeLayer = new CAShapeLayer();
            shapeLayer.Frame = controller.TabBar.Bounds;

            shapeLayer.Path = UIBezierPath.FromRoundedRect(
                controller.TabBar.Bounds,
                UIRectCorner.AllCorners,
                new CoreGraphics.CGSize(50, 50)).CGPath;

            controller.TabBar.Layer.Mask = shapeLayer;
        }

        private void AddMarginsToTheTabbar(UITabBarController controller)
        {
            var existingFrame = controller.TabBar.Frame;
            double margin = 50;

            var newX = existingFrame.X + margin;
            var newWidth = existingFrame.Width - (2 * margin);
            var newY = existingFrame.Y - margin;
            var newHeight = existingFrame.Height;

            var newRectFrame = new CGRect(newX, newY, newWidth, newHeight);
            controller.TabBar.Frame = newRectFrame;
        }
    }
}
