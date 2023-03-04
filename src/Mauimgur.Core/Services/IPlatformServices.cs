// <copyright file="IPlatformServices.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauimgur.Core.Models;

namespace Mauimgur.Core.Services
{
    /// <summary>
    /// Platform Services.
    /// </summary>
    public interface IPlatformServices
    {
        Task<IEnumerable<IMediaFile>> SelectFilesAsync();
    }
}
