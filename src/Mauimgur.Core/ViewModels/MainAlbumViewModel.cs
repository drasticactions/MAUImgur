// <copyright file="MainAlbumViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Drastic.ViewModels;

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
        }
    }
}