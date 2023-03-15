// <copyright file="AppDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.TrayWindow;
using Foundation;

namespace Mauimgur;

[Register("AppDelegate")]
public class AppDelegate : MauiTrayUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
