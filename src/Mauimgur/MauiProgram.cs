// <copyright file="MauiProgram.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Services;
using Mauimgur.Core.Services;
using Mauimgur.Core.ViewModels;
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

        var path = "database.db";
#if IOS
        path = System.IO.Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), "MAUImgur", "database.db");
        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);
#else
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
       .AddSingleton<MainAlbumViewModel>();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-regular-400.ttf", "FARegular");
                fonts.AddFont("fa-solid-900.ttf", "FASolid");
            });
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
