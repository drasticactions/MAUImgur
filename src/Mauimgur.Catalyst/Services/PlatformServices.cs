// <copyright file="PlatformServices.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Mauimgur.Core.Events;
using Mauimgur.Core.Models;
using Mauimgur.Core.Services;

namespace Mauimgur.Catalyst.Services
{
    internal class PlatformServices : IPlatformServices
    {
        private ImgurService imgur;
        private DatabaseService database;

        public PlatformServices(DatabaseService database, ImgurService imgur)
        {
            this.database = database;
            this.imgur = imgur;
        }

        public event EventHandler<UserLoginEventArgs>? OnUserAuthenticated;

        public async Task<IEnumerable<IMediaFile>> SelectFilesAsync()
        {
            IEnumerable<FileResult> results = await FilePicker.Default.PickMultipleAsync(new PickOptions() { });
            return results.Select(n => new MauiMediaFile(n));
        }

        public async Task StartAuthenticationAsync()
        {
            try
            {
                Microsoft.Maui.Authentication.WebAuthenticatorResult authResult = await WebAuthenticator.Default.AuthenticateAsync(
                    new Uri(this.imgur.AuthUrl),
                    new Uri("mauimgur://"));

                var test = new CustomWebAuthenticatorResult() { Properties = authResult.Properties };
                var token = await this.imgur.LoginWithWebAuthenticatorResultAsync(test);
                this.database.AddOrUpdateOauthTokenAsync(token).FireAndForgetSafeAsync();
                this.OnUserAuthenticated?.Invoke(this, new UserLoginEventArgs(token));

                // Do something with the token
            }
            catch (TaskCanceledException)
            {
                // Use stopped auth
            }
        }
    }
}
