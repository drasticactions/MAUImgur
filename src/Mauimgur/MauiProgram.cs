// <copyright file="MauiProgram.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;
using Mauimgur.Core.Services;
using Mauimgur.Core.ViewModels;
using Mauimgur.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.Maui.Storage;
using static System.Environment;

namespace Mauimgur;

/// <summary>
/// Main Maui Program.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Create MAUI app.
    /// </summary>
    /// <returns><see cref="MauiApp"/>.</returns>
    public static MauiApp CreateMauiApp()
    {
        Microsoft.Maui.Handlers.ButtonHandler.Mapper.AppendToMapping("CatalystButton", (handler, view) =>
        {
#if MACCATALYST
#pragma warning disable CA1416 // プラットフォームの互換性を検証
            handler.PlatformView.PreferredBehavioralStyle = UIKit.UIBehavioralStyle.Pad;
#pragma warning restore CA1416 // プラットフォームの互換性を検証
#endif
        });

        var path = "database.db";

#if MACCATALYST || IOS
        path = System.IO.Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), "MAUImgur", "database.db");
        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);
#elif ANDROID
        path = System.IO.Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), "MAUImgur", "database.db");
        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);
#elif WINDOWS
        path = System.IO.Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), "MAUImgur", "database.db");
        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);
#endif

        var builder = MauiApp.CreateBuilder();
        builder.Services!
       .AddSingleton<IErrorHandlerService, ErrorHandlerService>()
       .AddSingleton<IAppDispatcher, AppDispatcherService>()
       .AddSingleton<IPlatformServices, MauiPlatformServices>()
       .AddSingleton<ImgurService>(new ImgurService(Core.Utilities.Tokens.GetImgurClientId(), Core.Utilities.Tokens.GetImgurClientSecret()))
       .AddSingleton<DatabaseService>(new DatabaseService(path))
       .AddSingleton<ImageUploadViewModel>()
       .AddSingleton<MainAlbumViewModel>()
       ;

#if WINDOWS
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(window =>
                {
                    var manager = WinUIEx.WindowManager.Get(window);
                    manager.MinWidth = 640;
                    manager.MinHeight = 480;
                    manager.Backdrop = new WinUIEx.MicaSystemBackdrop();
                });
            });
        });
#endif

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
