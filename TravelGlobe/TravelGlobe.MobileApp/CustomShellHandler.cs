using Microsoft.Maui.Controls.Handlers;
using Microsoft.Maui.Controls;

namespace TravelGlobe.MobileApp;

public partial class CustomShellHandler : ShellHandler
{
    protected override void ConnectHandler(Shell view)
    {
        base.ConnectHandler(view);
        CustomizeTabBar();
    }

    partial void CustomizeTabBar();
}