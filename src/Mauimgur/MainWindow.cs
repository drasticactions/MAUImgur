// <copyright file="MainWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.DragAndDrop.Maui;
using Drastic.Tools;
using Drastic.TrayWindow;
using Mauimgur.Core.ViewModels;
using Microsoft.Maui.Platform;

namespace Mauimgur
{
    /// <summary>
    /// Main Window.
    /// </summary>
    public class MainWindow : DragAndDropWindow
    {
        private IServiceProvider provider;
        private ImageUploadViewModel imageUploadVM;
        private bool isBusy;
        private ImageUploadResultPage resultPage;

#if MACCATALYST
        private Drastic.DragAndDrop.DragAndDrop? DragAndDrop { get; set; }
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="page">Default Detail Page.</param>
        /// <param name="provider">IServiceProvider.</param>
        public MainWindow(Page page, IServiceProvider provider)
            : base(new Drastic.DragAndDrop.Maui.DragElementOverlay(Color.FromRgba(225, 0, 0, .2)))
        {
            this.Page = page;
            this.provider = provider;
            this.imageUploadVM = this.provider.GetService<ImageUploadViewModel>()!;
            this.resultPage = new ImageUploadResultPage(this.provider);
            this.imageUploadVM.ImageUploadProgress.ProgressChanged += this.ImageUploadProgress_ProgressChanged;
            this.Drop += this.MainWindow_Drop;
        }

#if MACCATALYST
        public Drastic.Tray.TrayIcon? Icon { get; set; }

        protected override void OnHandlerChanged()
        {
            var platform = (UIKit.UIWindow)this.Handler.PlatformView!;
            Drastic.TrayWindow.NSApplication.CloseWindow(platform).FireAndForgetSafeAsync();
            this.Icon = this.GenerateTrayIcon();
            this.Icon.RightClicked += (object? sender, Drastic.Tray.TrayClickedEventArgs e) => this.Icon.OpenMenu();
            base.OnHandlerChanged();
        }

#endif

        private async void MainWindow_Drop(object? sender, Drastic.DragAndDrop.DragAndDropOverlayTappedEventArgs e)
        {
            this.HandleDropEventsAsync(e).FireAndForgetSafeAsync();
        }

        private async Task HandleDropEventsAsync(Drastic.DragAndDrop.DragAndDropOverlayTappedEventArgs e)
        {
#if WINDOWS
                var storageFiles = new List<Windows.Storage.StorageFile>();
                foreach (var file in e.Paths) {
                    storageFiles.Add(await Windows.Storage.StorageFile.GetFileFromPathAsync(file));
                }

                var results = storageFiles.Select(n => new Mauimgur.Platforms.Windows.WindowsMediaFile(n));
#else
            var results = e.Paths.Select(n => new Mauimgur.Models.MauiMediaFile(new FileResult(n)));
#endif

            var test = results.FirstOrDefault();
            var fart = test!.OpenReadAsync();
            this.imageUploadVM.UploadMedia(results).FireAndForgetSafeAsync();
        }

        private void ImageUploadProgress_ProgressChanged(object? sender, Core.Models.ImageUploadUpdate e)
        {
            this.isBusy = !e.IsDone;
        }

#if MACCATALYST
        private Drastic.Tray.TrayIcon GenerateTrayIcon()
        {
            var menuItems = new List<Drastic.Tray.TrayMenuItem>();
            menuItems.Add(new Drastic.Tray.TrayMenuItem("Quit", null, async () => { Drastic.TrayWindow.NSApplication.Terminate(); }, "q"));
            var trayIcon = new Drastic.Tray.TrayIcon("MAUImgur", new Drastic.Tray.TrayImage(UIKit.UIImage.GetSystemImage("photo.circle")!), menuItems);
            if (UIKit.UIApplication.SharedApplication.Delegate is MauiTrayUIApplicationDelegate trayDelegate)
            {
                var control = this.Page!.ToUIViewController(Microsoft.Maui.Controls.Application.Current!.Handler.MauiContext!);
                this.DragAndDrop = new Drastic.DragAndDrop.DragAndDrop(control);
                this.DragAndDrop.Drop += this.MainWindow_Drop;
                trayDelegate.CreateTrayWindow(trayIcon, new TrayWindowOptions(), control, false);
            }
            else
            {
                throw new ArgumentException("You must set your AppDelegate to use Drastic.TrayWindow.TrayAppDelegate");
            }

            return trayIcon;
        }
#endif
    }
}