// <copyright file="MauiPlatformServices.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauimgur.Core.Models;
using Mauimgur.Core.Services;
using Mauimgur.Models;

namespace Mauimgur.Services
{
    /// <summary>
    /// MAUI Platform Services.
    /// </summary>
    public class MauiPlatformServices : IPlatformServices
    {
        /// <inheritdoc/>
        public async Task<IEnumerable<IMediaFile>> SelectFilesAsync()
        {
#if IOS
            var photo = await MediaPicker.Default.PickPhotoAsync(new MediaPickerOptions());
            return new List<IMediaFile>() { new MauiMediaFile(photo) };
#else
            IEnumerable<FileResult> results = await FilePicker.Default.PickMultipleAsync(new PickOptions() { });
            return results.Select(n => new MauiMediaFile(n));
#endif
        }
    }
}
