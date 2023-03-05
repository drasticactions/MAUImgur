// <copyright file="MauimgurViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.ViewModels;
using Mauimgur.Core.Services;

namespace Mauimgur.Core.ViewModels;

/// <summary>
/// Mauimgur Base View Model.
/// </summary>
public class MauimgurViewModel : BaseViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MauimgurViewModel"/> class.
    /// </summary>
    /// <param name="services">IServiceProvider.</param>
    public MauimgurViewModel(IServiceProvider services)
        : base(services)
    {
        this.Imgur = (ImgurService)this.Services.GetService(typeof(ImgurService))!;
        this.Platform = (IPlatformServices)this.Services.GetService(typeof(IPlatformServices))!;
        this.Database = (DatabaseService)this.Services.GetService(typeof(DatabaseService))!;
    }

    /// <summary>
    /// Gets the platform services.
    /// </summary>
    public IPlatformServices Platform { get; }

    /// <summary>
    /// Gets the database Service.
    /// </summary>
    public DatabaseService Database { get; }

    /// <summary>
    /// Gets the Imgur Service.
    /// </summary>
    public ImgurService Imgur { get; }
}