// <copyright file="ImgurServiceTests.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Imgur.API;
using Imgur.API.Endpoints;
using Mauimgur.Core.Models;
using Mauimgur.Core.Services;

namespace Mauimgur.Tests;

/// <summary>
/// Imgur Service Tests.
/// </summary>
public class ImgurServiceTests : IClassFixture<ImgurServiceSetup>
{
    private ImgurService imgurService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImgurServiceTests"/> class.
    /// </summary>
    /// <param name="setup">Setup.</param>
    public ImgurServiceTests(ImgurServiceSetup setup)
    {
        this.imgurService = setup.ImgurService;
    }

    [Fact]
    public async Task UploadImage()
    {
        using var token = new CancellationTokenSource();
        var progress = new Progress<ImageUploadUpdate>();
        progress.ProgressChanged += (sender, update) =>
        {
            Assert.NotNull(update.LastUpdated);
            Assert.NotNull(update.LastUpdated.Link);
            Assert.True(update.IsDone);
            token.Cancel();
        };

        await this.imgurService.UploadImageAsync(new TestMediaFile(@"Images/Image.png"), progress);
        Assert.True(await this.CompletedTask(10000, token.Token));
    }

    private class TestMediaFile : IMediaFile
    {

        public TestMediaFile(string path)
        {
            this.FullPath = path;
        }

        public string FullPath { get; }

        public string ContentType => throw new NotImplementedException();

        public string FileName => throw new NotImplementedException();

        public Task<Stream> OpenReadAsync() => Task.FromResult((Stream)File.OpenRead(this.FullPath));
    }

    private async Task<bool> CompletedTask(int timeout, CancellationToken token)
    {
        try
        {
            await Task.Delay(timeout, token);
        }
        catch (TaskCanceledException e)
        {
            return true;
        }

        return token.IsCancellationRequested;
    }
}