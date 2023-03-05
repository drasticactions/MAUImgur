// <copyright file="ImageUploadViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Drastic.ViewModels;
using Mauimgur.Core.Models;

namespace Mauimgur.Core.ViewModels;

/// <summary>
/// Image Upload View Model.
/// </summary>
public class ImageUploadViewModel : MauimgurViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ImageUploadViewModel"/> class.
    /// </summary>
    /// <param name="services">IServiceProvider.</param>
    public ImageUploadViewModel(IServiceProvider services)
        : base(services)
    {
        this.ImageUploadProgress = new Progress<ImageUploadUpdate>();
        this.SelectAndUploadMediaCommand = new AsyncCommand(this.SelectAndUploadMedia, null, this.Dispatcher, this.ErrorHandler);
        this.ImageUploadProgress.ProgressChanged += this.ImageUploadProgress_ProgressChanged;
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