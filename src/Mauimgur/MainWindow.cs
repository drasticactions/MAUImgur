// <copyright file="MainWindow.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Drastic.Tools;
using Mauimgur.Core.ViewModels;

namespace Mauimgur
{
    /// <summary>
    /// Main Window.
    /// </summary>
    public class MainWindow : Window
    {
        private IServiceProvider provider;
        private ImageUploadViewModel imageUploadVM;
        private bool isBusy;
        private ImageUploadResultPage resultPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        /// <param name="provider">IServiceProvider.</param>
        public MainWindow(IServiceProvider provider)
        {
            this.provider = provider;
            this.imageUploadVM = this.provider.GetService<ImageUploadViewModel>()!;
            this.resultPage = new ImageUploadResultPage(this.provider);
            this.imageUploadVM.ImageUploadProgress.ProgressChanged += this.ImageUploadProgress_ProgressChanged;
        }

        public void ShowResultScreen()
        {
            if (!this.Navigation.ModalStack.Contains(this.resultPage))
            {
                this.Navigation.PushModalAsync(this.resultPage).FireAndForgetSafeAsync();
            }
        }

        public void CloseResultScreen()
        {
            if (this.Navigation.ModalStack.Contains(this.resultPage))
            {
                this.Navigation.PopModalAsync().FireAndForgetSafeAsync();
            }
        }

        private void ImageUploadProgress_ProgressChanged(object? sender, Core.Models.ImageUploadUpdate e)
        {
            // If window is not busy, and upload is not done, that means
            // we have started an upload. So, show the result page.
            if (!this.isBusy && !e.IsDone)
            {
                this.isBusy = !e.IsDone;
                this.ShowResultScreen();
                return;
            }

            this.isBusy = !e.IsDone;
        }
    }
}