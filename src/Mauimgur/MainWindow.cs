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
            this.imageUploadVM.ImageUploadProgress.ProgressChanged += this.ImageUploadProgress_ProgressChanged;
            this.Drop += this.MainWindow_Drop;
        }

        private async void MainWindow_Drop(object? sender, Drastic.DragAndDrop.DragAndDropOverlayTappedEventArgs e)
        {
            this.HandleDropEventsAsync(e).FireAndForgetSafeAsync();
        }

        private async Task HandleDropEventsAsync(Drastic.DragAndDrop.DragAndDropOverlayTappedEventArgs e)
        {
            var results = e.Paths.Select(n => new Mauimgur.Core.Models.MauiMediaFile(new FileResult(n)));
            var test = results.FirstOrDefault();
            var fart = test!.OpenReadAsync();
            this.imageUploadVM.UploadMedia(results).FireAndForgetSafeAsync();
        }

        private void ImageUploadProgress_ProgressChanged(object? sender, Core.Models.ImageUploadUpdate e)
        {
            this.isBusy = !e.IsDone;
        }
    }
}