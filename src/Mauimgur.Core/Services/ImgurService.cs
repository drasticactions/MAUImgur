// <copyright file="ImgurService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using System.Net.Http;
using Imgur.API;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Imgur.API.Models;
using Mauimgur.Core.Events;
using Mauimgur.Core.Models;

namespace Mauimgur.Core.Services
{
    /// <summary>
    /// Imgur Service.
    /// </summary>
    public class ImgurService
    {
        private ApiClient apiClient;
        private HttpClient client;
        private ImageEndpoint imageEndpoint;
        private OAuth2Endpoint oauth2Endpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="ImgurService"/> class.
        /// </summary>
        /// <param name="clientId">Client Id.</param>
        /// <param name="clientSecret">Client Secret.</param>
        /// <param name="client">HttpClient.</param>
        public ImgurService(string clientId, string clientSecret, HttpClient? client = default)
        {
            this.apiClient = new ApiClient(clientId, clientSecret);
            this.client = client ?? new HttpClient();
            this.imageEndpoint = new ImageEndpoint(this.apiClient, this.client);
            this.oauth2Endpoint = new OAuth2Endpoint(this.apiClient, this.client);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImgurService"/> class.
        /// </summary>
        /// <param name="apiClient"><see cref="ApiClient"/>.</param>
        /// <param name="client">HttpClient.</param>
        public ImgurService(ApiClient apiClient, HttpClient? client = default)
        {
            this.apiClient = apiClient;
            this.client = client ?? new HttpClient();
            this.imageEndpoint = new ImageEndpoint(this.apiClient, this.client);
            this.oauth2Endpoint = new OAuth2Endpoint(this.apiClient, this.client);
        }

        public event EventHandler<ImageUploadedEventArgs>? OnImageUploaded;

        public string AuthUrl => this.oauth2Endpoint.GetAuthorizationUrl();

        public OAuth2Token? OAuth2Token => this.apiClient.OAuth2Token as Imgur.API.Models.OAuth2Token;

        public Task UploadImageAsync(IMediaFile fileStream, IProgress<ImageUploadUpdate>? totalProgress = null, string? album = null, string? name = null, string? title = null, string? description = null, int? bufferSize = 4096, CancellationToken cancellationToken = default(CancellationToken))
         => this.UploadImagesAsync(new List<IMediaFile>() { fileStream }, totalProgress, album, name, title, description, bufferSize, cancellationToken);

        public async Task UploadImagesAsync(IEnumerable<IMediaFile> fileStreams, IProgress<ImageUploadUpdate>? totalProgress = null, string? album = null, string? name = null, string? title = null, string? description = null, int? bufferSize = 4096, CancellationToken cancellationToken = default(CancellationToken))
        {
            var totalFiles = fileStreams.Count();
            var completedUploads = 0;
            foreach (var stream in fileStreams)
            {
                using var streamTest = await stream.OpenReadAsync();
                var result = await this.imageEndpoint.UploadImageAsync(streamTest, album, name, title, description, null, bufferSize, cancellationToken);
                completedUploads += 1;
                totalProgress?.Report(new ImageUploadUpdate(completedUploads, totalFiles, result));
                this.OnImageUploaded?.Invoke(this, new ImageUploadedEventArgs((Imgur.API.Models.Image)result));
            }
        }

        public async Task<OAuth2Token> LoginWithWebAuthenticatorResultAsync(CustomWebAuthenticatorResult result)
        {
            var token = new OAuth2Token {
                AccessToken = result.AccessToken,
                RefreshToken = result.RefreshToken,
                AccountId = Convert.ToInt32(result.Properties["account_id"]),
                AccountUsername = result.Properties["account_username"],
                ExpiresIn = result.ExpiresInNumber,
                TokenType = result.Properties["token_type"],
            };

            await this.LoginWithTokensAsync(token);

            return token;
        }

        public async Task LoginWithTokensAsync(OAuth2Token token)
        {
            this.apiClient.SetOAuth2Token(token);
        }
    }
}