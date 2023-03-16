// <copyright file="MainWindow.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Drastic.Tray;
using Drastic.TrayWindow;
using Mauimgur.Core.Pages;
using Mauimgur.Core.Services;
using Mauimgur.Core.ViewModels;
using Microsoft.Maui.Platform;
using Microsoft.UI.Xaml;

namespace Mauimgur.WinUI
{
    /// <summary>
    /// The main window for the application.
    /// </summary>
    public sealed partial class MainWindow : WinUITrayAcrylicWindow
    {
        private IMauiContext? context;
        private Drastic.DragAndDrop.DragAndDrop? dragAndDrop;

        private ImageUploadViewModel? imageUploadVM;
        private ImageGalleryPage? imageGalleryPage;
        private FrameworkElement? imageGalleryContent;

        private SettingsPage? settingsPage;
        private FrameworkElement? settingsPageContent;

        public MainWindow(TrayIcon icon, TrayWindowOptions options)
            : base(icon, options, true)
        {
            this.InitializeComponent();
        }

        public void SetupContent(IMauiContext context)
        {
            if (this.context is not null)
            {
                return;
            }

            this.context = context;
            this.imageUploadVM = context.Services.GetService<ImageUploadViewModel>()!;
            this.imageGalleryPage = new ImageGalleryPage(context.Services);
            this.imageGalleryContent = this.imageGalleryPage.ToPlatform(context);

            this.Content = this.imageGalleryContent;

            this.dragAndDrop = new Drastic.DragAndDrop.DragAndDrop(this);
            this.dragAndDrop.Drop += this.DragAndDrop_Drop;
        }

        private async void DragAndDrop_Drop(object? sender, Drastic.DragAndDrop.DragAndDropOverlayTappedEventArgs e)
        {
            if (this.imageUploadVM is null)
            {
                return;
            }

            var storageFiles = new List<Windows.Storage.StorageFile>();
            foreach (var file in e.Paths)
            {
                storageFiles.Add(await Windows.Storage.StorageFile.GetFileFromPathAsync(file));
            }

            var results = storageFiles.Select(n => new WindowsMediaFile(n));
            this.imageUploadVM.UploadMedia(results).FireAndForgetSafeAsync();
        }
    }
}
