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
        /// <param name="page">Default Detail Page.</param>
        /// <param name="provider">IServiceProvider.</param>
        public MainWindow(Page page, IServiceProvider provider)
        {
            this.Page = page;
            this.provider = provider;
            this.imageUploadVM = this.provider.GetService<ImageUploadViewModel>()!;
            this.resultPage = new ImageUploadResultPage(this.provider);
            this.imageUploadVM.ImageUploadProgress.ProgressChanged += this.ImageUploadProgress_ProgressChanged;
        }

        private void ImageUploadProgress_ProgressChanged(object? sender, Core.Models.ImageUploadUpdate e)
        {
            this.isBusy = !e.IsDone;
        }
    }
}