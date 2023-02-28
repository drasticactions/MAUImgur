// <copyright file="ImageUploadViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

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
    }

    /// <summary>
    /// Gets the Image Upload Update.
    /// </summary>
    public Progress<ImageUploadUpdate> ImageUploadProgress { get; }
}