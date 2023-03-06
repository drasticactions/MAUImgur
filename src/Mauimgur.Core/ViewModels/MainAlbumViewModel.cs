// <copyright file="MainAlbumViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Collections.ObjectModel;
using Drastic.Tools;
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
            this.Images = new ObservableCollection<Imgur.API.Models.Image>();
            this.Imgur.OnImageUploaded += this.Imgur_OnImageUploaded;
            this.OnLoad().FireAndForgetSafeAsync();
        }

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
    }
}