// <copyright file="MauiPlatformServices.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauimgur.Core.Models;
using Mauimgur.Core.Services;
using Mauimgur.Models;

namespace Mauimgur.Services
{
    public class MauiPlatformServices : IPlatformServices
    {
        public async Task<IEnumerable<IMediaFile>> SelectFilesAsync()
        {
            IEnumerable<FileResult> results = await FilePicker.Default.PickMultipleAsync(new PickOptions() { });
            return results.Select(n => new MauiMediaFile(n));
        }
    }
}
