// <copyright file="MainAlbumViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.ObjectModel;
using Drastic.Tools;
using Drastic.ViewModels;
using Mauimgur.Core.Models;

namespace Mauimgur.Core.ViewModels
{
    /// <summary>
    /// Main Album View Model.
    /// </summary>
    public class MainAlbumViewModel : MauimgurViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainAlbumViewModel"/> class.
        /// </summary>
        /// <param name="services">IServiceProvider.</param>
        public MainAlbumViewModel(IServiceProvider services)
            : base(services)
        {
            this.Images = new ObservableCollection<Imgur.API.Models.Image>();
            this.AuthenticationCommand = new AsyncCommand(this.Platform.StartAuthenticationAsync, null, this.Dispatcher, this.ErrorHandler);
            this.Imgur.OnImageUploaded += this.Imgur_OnImageUploaded;
            this.ImageUploadProgress = new Progress<ImageUploadUpdate>();
            this.SelectAndUploadMediaCommand = new AsyncCommand(this.SelectAndUploadMedia, null, this.Dispatcher, this.ErrorHandler);
            this.ImageUploadProgress.ProgressChanged += this.ImageUploadProgress_ProgressChanged;
            this.OnLoad().FireAndForgetSafeAsync();
        }

        public AsyncCommand AuthenticationCommand { get; }

        /// <summary>
        /// Gets the images.
        /// </summary>
        public ObservableCollection<Imgur.API.Models.Image> Images { get; }

        /// <inheritdoc/>
        public override async Task OnLoad()
        {
            await base.OnLoad();
            if (!this.Images.Any())
            {
                foreach (var image in this.Database.Images!)
                {
                    this.Images.Add(image);
                }
            }
        }

        private void Imgur_OnImageUploaded(object? sender, Events.ImageUploadedEventArgs e)
        {
            if (e.Image != null)
            {
                this.Images.Add(e.Image!);
            }
        }

        /// <summary>
        /// Gets the select and upload media command.
        /// </summary>
        public AsyncCommand SelectAndUploadMediaCommand { get; }

        /// <summary>
        /// Gets the Image Upload Update.
        /// </summary>
        public Progress<ImageUploadUpdate> ImageUploadProgress { get; }

        private async Task SelectAndUploadMedia()
        {
            var files = await this.Platform.SelectFilesAsync();
            await this.UploadMedia(files);
        }

        public async Task UploadMedia(IEnumerable<IMediaFile> files)
        {
            await this.Imgur.UploadImagesAsync(files, this.ImageUploadProgress);
        }

        private Task<int> SaveImageToDatabaseAsync(Imgur.API.Models.Image image)
        {
            return this.Database.AddOrUpdateImageAsync(image);
        }

        private void ImageUploadProgress_ProgressChanged(object? sender, ImageUploadUpdate e)
        {
            this.IsBusy = !e.IsDone;

            if (e.LastUpdated is not null)
            {
                this.SaveImageToDatabaseAsync((Imgur.API.Models.Image)e.LastUpdated).FireAndForgetSafeAsync(this.ErrorHandler);
            }
        }
    }
}