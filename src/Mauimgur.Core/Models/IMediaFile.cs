// <copyright file="IMediaFile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Mauimgur.Core.Models
{
    public interface IMediaFile
    {
        /// <summary>
        ///  Gets the full path and filename.
        /// </summary>
        public string FullPath { get; }

        /// <summary>
        ///  Gets the file's content type as a MIME type (e.g.: image/png).
        /// </summary>
        public string ContentType { get; }

        /// <summary>
        /// Gets the filename for this file.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Opens a System.IO.Stream to the corresponding file on the filesystem.
        /// </summary>
        /// <returns>A System.IO.Stream containing the file data.</returns>
        public Task<Stream> OpenReadAsync();
    }
}
