// <copyright file="MauiMediaFile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauimgur.Core.Models;

namespace Mauimgur.Core.Models
{
    /// <summary>
    /// MAUI Media File.
    /// </summary>
    public class MauiMediaFile : IMediaFile
    {
        private FileResult file;

        /// <summary>
        /// Initializes a new instance of the <see cref="MauiMediaFile"/> class.
        /// </summary>
        /// <param name="file">MAUI File Result.</param>
        public MauiMediaFile(FileResult file)
        {
            this.file = file;
        }

        /// <inheritdoc/>
        public string FullPath => this.file.FullPath;

        /// <inheritdoc/>
        public string ContentType => this.file.ContentType;

        /// <inheritdoc/>
        public string FileName => this.file.FileName;

        /// <inheritdoc/>
        public Task<Stream> OpenReadAsync() => this.file.OpenReadAsync();
    }
}
