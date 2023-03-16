// <copyright file="AppDelegate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using CoreFoundation;
using Drastic.Services;
using Drastic.Tools;
using Drastic.Tray;
using Drastic.TrayWindow;
using Foundation;
using Mauimgur.Catalyst.Services;
using Mauimgur.Core.Models;
using Mauimgur.Core.Services;
using Mauimgur.Core.ViewModels;
using Microsoft.Maui.Embedding;
using Microsoft.Maui.Platform;
using UIKit;
using static System.Environment;

namespace Mauimgur.Catalyst;

[Register("AppDelegate")]
public class AppDelegate : TrayAppDelegate
{
    private MauiContext? _mauiContext;
    private Mauimgur.Core.Pages.ImageGalleryPage? page;
    private UIViewController? controller;
    private Drastic.DragAndDrop.DragAndDrop? dragAndDrop;
    private ImageUploadViewModel? uploadViewModel;

    public override UIWindow? Window
    {
        get;
        set;
    }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        var path = System.IO.Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), "MAUImgur", "database.db");
        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);

        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();

        builder.Services.AddSingleton<IErrorHandlerService, ErrorHandlerService>()
            .AddSingleton<IAppDispatcher, Mauimgur.Catalyst.Services.AppDispatcher>()
            .AddSingleton<IPlatformServices, PlatformServices>()
            .AddSingleton<ImgurService>(new ImgurService(Core.Utilities.Tokens.GetImgurClientId(), Core.Utilities.Tokens.GetImgurClientSecret()))
            .AddSingleton<DatabaseService>(new DatabaseService(path))
            .AddSingleton<ImageUploadViewModel>()
            .AddSingleton<MainAlbumViewModel>();

        MauiApp mauiApp = builder.Build();
        this._mauiContext = new MauiContext(mauiApp.Services);

        var menuItems = new List<TrayMenuItem>();
        var trayImage = new TrayImage(UIImage.GetSystemImage("photo.circle")!);
        var icon = new Drastic.Tray.TrayIcon(Core.Translations.Common.AppTitle, trayImage);
        menuItems.Add(new TrayMenuItem(Core.Translations.Common.QuitMenuItem, null, async () => { Drastic.TrayWindow.NSApplication.Terminate(); }, "q"));
        icon.RightClicked += (object? sender, TrayClickedEventArgs e) => icon.OpenMenu();
        this.page = new Mauimgur.Core.Pages.ImageGalleryPage(this._mauiContext.Services);
        this.controller = this.page.ToUIViewController(this._mauiContext);
        this.CreateTrayWindow(icon, new TrayWindowOptions(), this.controller);

        this.uploadViewModel = this._mauiContext.Services.GetService<ImageUploadViewModel>()!;

        this.dragAndDrop = new Drastic.DragAndDrop.DragAndDrop(this.controller);
        this.dragAndDrop.Drop += this.DragAndDrop_Drop;
        return true;
    }

    private void DragAndDrop_Drop(object? sender, Drastic.DragAndDrop.DragAndDropOverlayTappedEventArgs e)
    {
        if (this.uploadViewModel is null)
        {
            return;
        }

        var results = e.Paths.Select(n => new MauiMediaFile(new FileResult(n)));
        this.uploadViewModel.UploadMedia(results).FireAndForgetSafeAsync();
    }
}