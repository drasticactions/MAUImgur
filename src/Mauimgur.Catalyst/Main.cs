using Mauimgur.Catalyst;
using UIKit;

[assembly: ExportFont("fa-regular-400.ttf", Alias = "FARegular")]
[assembly: ExportFont("fa-solid-900.ttf", Alias = "FASolid")]

// This is the main entry point of the application.
// If you want to use a different Application Delegate class from "AppDelegate"
// you can specify it here.
UIApplication.Main (args, null, typeof (AppDelegate));
