// <copyright file="ImageUploadedEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Imgur.API.Models;

namespace Mauimgur.Core.Events
{
    /// <summary>
    /// Image Uploaded Event Arguments.
    /// </summary>
    public class ImageUploadedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageUploadedEventArgs"/> class.
        /// </summary>
        /// <param name="image">Uploaded Image.</param>
        public ImageUploadedEventArgs(Image image)
        {
            this.Image = image;
        }

        /// <summary>
        /// Gets the image that was uploaded.
        /// </summary>
        public Image Image { get; }
    }
}