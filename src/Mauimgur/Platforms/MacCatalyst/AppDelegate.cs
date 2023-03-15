// <copyright file="AppDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.TrayWindow;
using Foundation;
using UIKit;

namespace Mauimgur;

[Register("AppDelegate")]
public class AppDelegate : MauiTrayUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        NSApplication.SetActivationPolicy(NSApplicationActivationPolicy.Accessory);

        return base.FinishedLaunching(application, launchOptions);
    }
}
