// <copyright file="ImgurService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Imgur.API;
using Imgur.API.Authentication;
using Imgur.API.Endpoints;
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
        }

        public Task UploadImageAsync(IMediaFile fileStream, IProgress<ImageUploadUpdate>? totalProgress = null, string? album = null, string? name = null, string? title = null, string? description = null, int? bufferSize = 4096, CancellationToken cancellationToken = default(CancellationToken))
         => this.UploadImagesAsync(new List<IMediaFile>() { fileStream }, totalProgress, album, name, title, description, bufferSize, cancellationToken);

        public async Task UploadImagesAsync(IEnumerable<IMediaFile> fileStreams, IProgress<ImageUploadUpdate>? totalProgress = null, string? album = null, string? name = null, string? title = null, string? description = null, int? bufferSize = 4096, CancellationToken cancellationToken = default(CancellationToken))
        {
            var totalFiles = fileStreams.Count();
            var completedUploads = 0;
            foreach (var stream in fileStreams)
            {
                var result = await this.imageEndpoint.UploadImageAsync(await stream.OpenReadAsync(), album, name, title, description, null, bufferSize, cancellationToken);
                completedUploads += 1;
                totalProgress?.Report(new ImageUploadUpdate(completedUploads, totalFiles, result));
            }
        }
    }
}