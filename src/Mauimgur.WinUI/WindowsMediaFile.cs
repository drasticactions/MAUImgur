// <copyright file="WindowsMediaFile.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauimgur.Core.Models;
using Windows.Storage;

namespace Mauimgur.WinUI
{
    public class WindowsMediaFile : IMediaFile
    {
        public WindowsMediaFile(IStorageFile file)
        {
            this.file = file;
        }

        public string FullPath => file.Path;

        public string ContentType => file.ContentType;

        public string FileName => file.Name;

        private IStorageFile file;

        public async Task<Stream> OpenReadAsync()
        {
            return (await file.OpenReadAsync()).AsStream();
        }
    }
}
