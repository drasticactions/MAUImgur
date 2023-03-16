// <copyright file="ImageUploadUpdate.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Imgur.API.Models;

namespace Mauimgur.Core.Models
{
    public class ImageUploadUpdate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadUpdate"/> class.
        /// </summary>
        /// <param name="uploadsCompleted">Number of Feeds Completed.</param>
        /// <param name="totalImages">Total Feeds.</param>
        /// <param name="lastUpdated">Last Feed Updated.</param>
        public ImageUploadUpdate(int uploadsCompleted, int totalImages, Imgur.API.Models.IImage? lastUpdated = default)
        {
            this.LastUpdated = lastUpdated;
            this.TotalImages = totalImages;
            this.UploadsCompleted = uploadsCompleted;
        }

        /// <summary>
        /// Gets the last image update.
        /// </summary>
        public Imgur.API.Models.IImage? LastUpdated { get; }

        /// <summary>
        /// Gets the total number of images.
        /// </summary>
        public int TotalImages { get; }

        /// <summary>
        /// Gets the number of uploads completed.
        /// </summary>
        public int UploadsCompleted { get; }

        /// <summary>
        /// Gets a value indicating whether the update is done.
        /// </summary>
        public bool IsDone => this.UploadsCompleted >= this.TotalImages;

        /// <summary>
        /// Gets a value indicating whether to fire a refresh.
        /// </summary>
        public bool FireRefresh { get; }
    }
}