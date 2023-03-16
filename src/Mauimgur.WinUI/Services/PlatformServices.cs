// <copyright file="PlatformServices.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Mauimgur.Core.Events;
using Mauimgur.Core.Models;
using Mauimgur.Core.Services;

namespace Mauimgur.WinUI.Services
{
    internal class PlatformServices : IPlatformServices
    {
        public event EventHandler<UserLoginEventArgs>? OnUserAuthenticated;

        public Task<IEnumerable<IMediaFile>> SelectFilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task StartAuthenticationAsync()
        {
            throw new NotImplementedException();
        }
    }
}
