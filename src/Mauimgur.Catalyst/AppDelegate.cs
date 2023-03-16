using Drastic.Tray;
using Drastic.TrayWindow;
using Foundation;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using UIKit;

namespace Mauimgur.Catalyst;

[Register("AppDelegate")]
public class AppDelegate : TrayAppDelegate
{
    MauiContext? _mauiContext;
    MainTabbedPage? page;
    UIViewController? controller;
    Drastic.DragAndDrop.DragAndDrop? dragAndDrop;

    public override UIWindow? Window
    {
        get;
        set;
    }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        // Perform your normal iOS registration

        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();

        // Register the Window
        // builder.Services.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(typeof(UIWindow), this.Window!));
        MauiApp mauiApp = builder.Build();
        _mauiContext = new MauiContext(mauiApp.Services);

        var page = new MainPage(_mauiContext.Services);

        var trayImage = new TrayImage(UIImage.GetSystemImage("photo.circle")!);
        var icon = new Drastic.Tray.TrayIcon("MAUImgur", trayImage);
        this.page = new MainTabbedPage(_mauiContext.Services);
        this.controller = this.page.ToUIViewController(_mauiContext);
        this.CreateTrayWindow(icon, new TrayWindowOptions(), this.controller);
        return true;
    }
}