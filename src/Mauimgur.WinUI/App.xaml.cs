// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Reflection;
using Drastic.Services;
using Drastic.Tray;
using Drastic.TrayWindow;
using Mauimgur.Core.Services;
using Mauimgur.Core.ViewModels;
using Mauimgur.WinUI.Services;
using Microsoft.Maui.Embedding;
using static System.Environment;

namespace Mauimgur.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Microsoft.UI.Xaml.Application
    {
        public IMauiContext? _mauiContext;
        private TrayIcon? icon;
        private MainWindow? window;

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var trayImage = new TrayImage(System.Drawing.Image.FromStream(GetResourceFileContent("TrayIcon.ico")!));
            var menuItems = new List<TrayMenuItem>
            {
                new TrayMenuItem(Mauimgur.Core.Translations.Common.QuitMenuItem, trayImage, async () => {
                    Microsoft.UI.Xaml.Application.Current.Exit();
                }),
            };

            this.icon = new TrayIcon(Core.Translations.Common.AppTitle, trayImage, menuItems);
            this.window = new MainWindow( icon, new TrayWindowOptions());
            this.icon.LeftClicked += (object? sender, TrayClickedEventArgs e) =>
            {
                this.window.ToggleVisibility();
            };

            var path = System.IO.Path.Combine(Environment.GetFolderPath(SpecialFolder.LocalApplicationData), "MAUImgur", "database.db");
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path)!);
            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiEmbedding<Microsoft.Maui.Controls.Application>();
            builder.Services.AddSingleton<IErrorHandlerService, ErrorHandlerService>()
            .AddSingleton<IAppDispatcher>(new AppDispatcher(dispatcherQueue))
            .AddSingleton<IPlatformServices, PlatformServices>()
            .AddSingleton<ImgurService>(new ImgurService(Core.Utilities.Tokens.GetImgurClientId(), Core.Utilities.Tokens.GetImgurClientSecret()))
            .AddSingleton<DatabaseService>(new DatabaseService(path))
            .AddSingleton<ImageUploadViewModel>()
            .AddSingleton<MainAlbumViewModel>();
            MauiApp mauiApp = builder.Build();
            _mauiContext = new MauiContext(mauiApp.Services);

            // Try to add Navigation to an existing window that was not created by MAUI itself.
            Type extensionType = Assembly.GetAssembly(typeof(Microsoft.Maui.MauiContext))!.GetType("Microsoft.Maui.MauiContextExtensions")!;
            MethodInfo method = extensionType.GetMethod("MakeWindowScope", BindingFlags.Public | BindingFlags.Static)!;
            var scope = _mauiContext.Services.CreateScope();
            _mauiContext = (IMauiContext?)method.Invoke(null, new object[] { _mauiContext, this.window, scope } )!;

            this.window.SetupContent(this._mauiContext);
        }

        /// <summary>
        /// Get Resource File Content via FileName.
        /// </summary>
        /// <param name="fileName">Filename.</param>
        /// <returns>Stream.</returns>
        public static Stream? GetResourceFileContent(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Mauimgur.WinUI." + fileName;
            if (assembly is null)
            {
                return null;
            }

            return assembly.GetManifestResourceStream(resourceName);
        }
    }
}
