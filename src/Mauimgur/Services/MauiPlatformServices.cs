// <copyright file="MauiPlatformServices.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.Tools;
using Mauimgur.Core.Events;
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
        private ImgurService imgur;
        private DatabaseService database;

        public MauiPlatformServices(DatabaseService database, ImgurService imgur)
        {
            this.database = database;
            this.imgur = imgur;
        }

        public event EventHandler<UserLoginEventArgs>? OnUserAuthenticated;

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

        /// <inheritdoc/>
        public async Task StartAuthenticationAsync()
        {
#if WINDOWS
            await Windows.System.Launcher.LaunchUriAsync(new Uri(this.imgur.AuthUrl));
#else
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
#endif
        }
    }
}
